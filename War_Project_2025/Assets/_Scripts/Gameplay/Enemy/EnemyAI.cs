using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public float damage = 10f;
    public float attackRange = 1.3f;
    public float attackCooldown = 1.5f;

    [Header("Components")]
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    private float lastAttackTime = 0f;
    private bool isDead = false;
    private bool isAttacking = false;

    [SerializeField] private GameObject sword;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;

        float multiplier = GameManager.Instance.difficulty;

        // Apply difficulty
        damage *= multiplier;
        agent.speed *= multiplier;

        
    }

    private void Update()
    {
        if (player == null || isDead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (!isAttacking && distance > attackRange)
        {
           
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("IsRunning", true);
        }
        else if (!isDead)
        {
            // Stop moving and attack
            agent.isStopped = true;
            animator.SetBool("IsRunning", false);

            if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }


    private void Attack()
    {
        
        animator.SetTrigger("Attack");
    }
    public void EnableDealDmg()
    {
        if (sword != null)
        {
            sword.GetComponent<Collider>().enabled = true;
            sword.GetComponent<Collider>().isTrigger = true;

        }
    }
    public void DisableDealDmg() 
    {
        if (sword != null)
        {
            sword.GetComponent<Collider>().enabled = false;
            sword.GetComponent<Collider>().isTrigger = false;
           
        }
    }

   public void GetHitAnimationEventStart() 
    {
        isAttacking = false;
        if (sword != null)
        {
            sword.GetComponent<Collider>().enabled = false;
            sword.GetComponent<Collider>().isTrigger = false;

        }
        
    }
    public void OnEnemyDeath()
    {
        isDead = true;
        isAttacking = false;
        agent.isStopped = true;
        if (sword != null)
        {
            sword.GetComponent<Collider>().enabled = false;
            sword.GetComponent<Collider>().isTrigger = false;

        }
    }
    public float GetDamage()
    {
        return damage;
    }
    public void OnAttackAnimationStart()
    {
        isAttacking = true;
        agent.isStopped = true;
    }
    public void OnAttackAnimationEnd()
    {
        isAttacking = false;
    }

}
