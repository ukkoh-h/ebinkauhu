using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
    
#endif
    public class ThirdPersonController : MonoBehaviour, IDataPersistence
    {
        public monster monster;
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;
        
        [Tooltip("Reverse move speed of the character in m/s")]
        public float ReverseSpeed = 1.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("Tank controls turning speed")]
        public float turnSpeed = 180f;

        [Tooltip("Rverse turning speed")]
        public float turnReverseSpeed = 90f;

        [Tooltip("turning speed while static")]
        public float turnStaticSpeed = 180f;

        [Tooltip("turning speed while sprinting")]
        public float turnSprintSpeed = 140f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;
        
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        // player
        private float _speed;
        private float _animationBlend;
        //private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDMotionSpeed;

#if ENABLE_INPUT_SYSTEM 
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private bool _hasAnimator;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }

        private InputAction moveAction;
        InputAction qt;
        private Quaternion targetRotation;

        private void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM 
            _playerInput = GetComponent<PlayerInput>();
#endif

            AssignAnimationIDs();
            _fallTimeoutDelta = FallTimeout;
            moveAction = _playerInput.actions["Move"];
            targetRotation = transform.rotation;
            qt = _playerInput.actions.FindAction("Quickturn");
        
        }

        public void LoadData(GameData data)
        {
            this.transform.position = data.playerPosition;
        }

        public void SaveData(ref GameData data)
        {
            data.playerPosition = this.transform.position;
        }

        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);
            JumpAndGravity();
            GroundedCheck();
            Move();
            if (_speed > (SprintSpeed - 0.5)) {
                monster.MakeNoise();
            }
        }
        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);
        }

        private void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
            float targetTurnSpeed = _input.sprint ? turnSprintSpeed : turnSpeed;

            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            float inputHorizontal = moveInput.x;
            float inputVertical = moveInput.y;
            if (inputVertical == 0) 
            {
                targetTurnSpeed = turnStaticSpeed;
            }
            if (_input.move == Vector2.down || _input.sprint && inputVertical < 0 || inputHorizontal != 0 && inputVertical < 0 ) 
            {
                targetSpeed = ReverseSpeed;
                targetTurnSpeed = turnReverseSpeed;
            }

            /* float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
            float targetTurnSpeed = _input.sprint ? turnSprintSpeed : turnWalkSpeed;

            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            float inputHorizontal = moveInput.x;
            float inputVertical = moveInput.y;
            if (inputVertical == 0) targetTurnSpeed = turnSpeed;
            if (_input.move == Vector2.down || _input.sprint && inputVertical < 0 || inputHorizontal != 0 && inputVertical < 0 ) 
            {
                targetSpeed = ReverseSpeed;
                targetTurnSpeed = turnWalkSpeed;
            } */

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // normalise input direction
            //Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            /* if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            } */


            //Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            /* _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude); */

                //float targetTurnStaticSpeed = inputVertical? turnStaticSpeed: turnSpeed;


                /* if (){
                    transform.Rotate(0, inputHorizontal * targetTurnSpeed * Time.deltaTime, 0);
                }
                else{
                    
                } */
                if (_input.sprint) _animationBlend = 5.335f; else _animationBlend = 2.0f;
                if (inputVertical == 0) 
                {
                    if (_input.move == Vector2.zero) 
                    {
                        _animationBlend = 0f;
                    } else {
                        _animationBlend = 2f;
                    }

                    

                    _animator.SetFloat(_animIDSpeed, _animationBlend);
                    _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
                }

            if (_input.move == Vector2.down || _input.sprint && inputVertical < 0 || inputHorizontal != 0 && inputVertical < 0 ) 
            {
                _animationBlend = 1.75f;
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
                

                if (_input.move == Vector2.down || inputHorizontal != 0 && inputVertical < 0 ) 
                    transform.Rotate(0, -inputHorizontal * targetTurnSpeed * Time.deltaTime, 0);
                else 
                    transform.Rotate(0, inputHorizontal * targetTurnSpeed * Time.deltaTime, 0);
                
                Vector3 movDir = transform.forward * inputVertical * targetSpeed;
                
                _controller.Move(movDir * Time.deltaTime - Vector3.up * 0.1f);
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);

                /* if (inputVertical < 0) // Example input condition
                {
                    // Set the target rotation to 180 degrees around the Y axis
                    targetRotation *= Quaternion.Euler(0, 180, 0); 
                } */

                // Smoothly interpolate towards the target rotation every frame
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smooth * Time.deltaTime);
                if(qt.triggered)
                {
                    //transform.rotation*Quaternion.AngleAxis(180, Vector3.up);
                    transform.rotation*=Quaternion.AngleAxis(180, Vector3.up);
                    
                    //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f);
                }
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }
            }
            else
            {
                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
            }
            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }
    }
}