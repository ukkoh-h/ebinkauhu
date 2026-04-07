using System.Diagnostics;
//using System.Threading.Tasks.Dataflow;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
//using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class monster : MonoBehaviour
{
    // Joni Koodi
    public float health = 15;
    public float maxHealth = 15;
    public float cooldown = 8f;

    public bool hitOnce = false;
    public bool hitTwice = false;
    public bool disable = false;

    public AudioClip[] MonsterAudioClips;
    [Range(0, 1)] public float MonsterAudioVolume = 0.5f;

    //private float _animationBlend;

    //private int _animIDSpeed;
    //private int _animIDMotionSpeed;

    //private Animator _animator;
    //private bool _hasAnimator;

    public Swinger swinger;
    public NavMeshAgent navAgent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    //public float health;
    public float walkPointRange;
    public float patrolSpeed;
    public float chaseSpeed;
    public float timeBetweenAttacks;
    public float sightRange;
    public float hearingRange;
    /// <summary>
    public float attackRange;
    public bool cantSee = false;
    /// </summary>
    /*public int damage;
    public Animator animator;
    public ParticleSystem hitEffect;*/

    
    private Vector3 walkPoint;
    private bool walkPointSet;
    private Vector3 targetPosition;
    private bool alreadyAttacked;
    private bool noise = false;
    private bool playerSeen = false;
    private bool playerHeared = false;
    private bool navigating = false;
    /*private bool takeDamage;*/

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        
    }

    private void Start()
    {
        //_hasAnimator = TryGetComponent(out _animator);
        //AssignAnimationIDs();
    }

    private void Update()
    {
        MonsterHealth();
        //_hasAnimator = TryGetComponent(out _animator);

        bool playerInHearingRange = Physics.CheckSphere(transform.position, hearingRange, playerLayer);
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if(!disable) {
            
            //Debug.Log(walkPointSet);

            if (playerInHearingRange && noise) playerHeared = true;
            if (playerInSightRange && !cantSee)
            {
                playerSeen = true;
            }
            else
            {
                playerSeen = false;
            }
            /*if (playerHeared && playerInSightRange) {playerHeared = false;}
            if (playerInSightRange) {Debug.Log("I SEE YOU!");}*/
            
            if (!playerSeen && !playerInAttackRange && !playerHeared)
            {
                Patroling();
                navAgent.speed = patrolSpeed;

            }
            else if (!playerSeen && !playerInAttackRange && playerHeared)
            {
                NavLastHeard();
                navAgent.speed = chaseSpeed;
            }
            else  if (playerSeen && !playerInAttackRange)
            {
                ChasePlayer();
                navAgent.speed = chaseSpeed;
            }
            else if (playerInAttackRange && playerSeen)
            {
                AttackPlayer();
            }
            //else if (!playerInSightRange /*&& takeDamage*/)
            //{
            //    ChasePlayer();
            //}
        }
        
    }
    /* private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    } */

    private void MonsterHealth() 
    {
        if(health == (maxHealth / 3) * 2 )
        {
            if(hitOnce == false) 
            {
                OnHit();
                hitOnce = true;
            }
        }
        
        if(health == maxHealth / 3)
        {
            if(hitTwice == false) 
            {
                OnHit();
                hitTwice = true;
            }
        }

        if(health <= 0)
        {
            if (MonsterAudioClips.Length > 0) OnHit();

            disable = true;
            Invoke("Activate", cooldown);
        }
    }

    private void OnHit()
    {
        if (MonsterAudioClips.Length > 0)
            {
                var index = Random.Range(0, MonsterAudioClips.Length);
                AudioSource.PlayClipAtPoint(MonsterAudioClips[index], transform.TransformPoint(this.transform.position), MonsterAudioVolume);
            }
    }

    void Activate()
    {
        disable = false;
        health = maxHealth;
        hitOnce = false;
        hitTwice = false;
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            navAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //animator.SetFloat("Velocity", 0.2f);

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(player.position.x + randomX, player.position.y + 1, player.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }

    private void NavLastHeard()
    {
        if (!navigating) {
            targetPosition = player.position;
            navigating = true;
            noise = false;
        }
        navAgent.SetDestination(targetPosition);

        Vector3 distanceToTargetPosition = transform.position - targetPosition;

        //Debug.Log(distanceToTargetPosition.magnitude);

        if (distanceToTargetPosition.magnitude < 2)
        {
            playerHeared = false;
            //Debug.Log(playerHeared);
        }
    }

   private void ChasePlayer()
    {
        navAgent.SetDestination(player.position);
        //animator.SetFloat("Velocity", 0.6f);
        navAgent.isStopped = false; // Add this line
    }


    private void AttackPlayer()
    {
        navAgent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            swinger.Swing();
            transform.LookAt(player.position);
            alreadyAttacked = true;
            //animator.SetBool("Attack", true);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            /*RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
            {
            
                //    YOU CAN USE THIS TO GET THE PLAYER HUD AND CALL THE TAKE DAMAGE FUNCTION

                //PlayerHUD playerHUD = hit.transform.GetComponent<PlayerHUD>();
                //if (playerHUD != null)
                //{
                //   playerHUD.takeDamage(damage);
                //}
         
            }*/
    }
}


    private void ResetAttack()
    {
        alreadyAttacked = false;
        //animator.SetBool("Attack", false);
    }

    public void MakeNoise()
    {
        noise = true;
        navigating = false;
    }
    public void RespawnMonster()
    {
        transform.position = new Vector3(-5, 0, -13);
    }
        public void SpawnMonsterBehindWall()
    {
        transform.position = new Vector3(-2, 3, -7);
    }

    /*public void TakeDamage(float damage)
    {
        health -= damage;
        //hitEffect.Play();
        StartCoroutine(TakeDamageCoroutine());

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private IEnumerator TakeDamageCoroutine()
    {
        takeDamage = true;
        yield return new WaitForSeconds(2f);
        takeDamage = false;
    }

    private void DestroyEnemy()
    {
        StartCoroutine(DestroyEnemyCoroutine());
    }

    private IEnumerator DestroyEnemyCoroutine()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }*/
}

