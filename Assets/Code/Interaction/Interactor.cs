using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    InputAction interact;

    private PlayerInput _playerInput;

    [SerializeField] Animator vampyr;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        interact = _playerInput.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (interact.WasPerformedThisFrame())
        {
            Ray r = new(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                    vampyr.Play("Interact", 0, 0.25f);
                    
                    _playerInput.enabled = false;
                    Invoke("Activate", 0.7f);
                }
            }
        }
    }
    void Activate()
    {
        _playerInput.enabled = true;
    }
}