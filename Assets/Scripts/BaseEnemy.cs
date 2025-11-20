using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public float attackCooldown = 2f;

    private float attackTimer;
    private NavMeshAgent agent;
    public Animator animator;
    public Transform target;

    private bool isAttacking = false;
    private bool damageDealt;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        target = GetComponent<ObjectDetectionRadius>().GetNearestGameobject();

        if (target != null)
        {
            agent.SetDestination(target.position);

            // direction only on XZ plane
            Vector3 dir = target.position - transform.position;
            dir.y = 0f;

            // rotate player
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 10f);





            animator.SetBool("isMoving", agent.velocity.magnitude > 0.1f);
            agent.isStopped = false;
            // Check if in attack range
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                TryAttack();
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
            agent.isStopped = true;
        }

        HandleAttackState(); // Manage attack timing WITHOUT animation events
    }

    private void TryAttack()
    {
        if (!isAttacking && attackTimer <= 0)
        {
            damageDealt = false;
            isAttacking = true;
            agent.isStopped = true;
            animator.SetTrigger("Attack");
            attackTimer = attackCooldown; // Start cooldown
        }
    }

    private void HandleAttackState()
    {
        if (isAttacking)
        {
            // Get animation length
            AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (animInfo.IsName("Attack") && animInfo.normalizedTime >= 1f)
            {
                // Animation finished
                isAttacking = false;
                agent.isStopped = false;

            }
            if (animInfo.IsName("Attack") && animInfo.normalizedTime >= 0.4f && !damageDealt)
            {
                Debug.Log("PlayerHealthDecrease");
                damageDealt = true;
            }
        }
        else
        {
            attackTimer -= Time.deltaTime; // Cooldown timer
        }


    }

    
}
