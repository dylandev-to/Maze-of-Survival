using UnityEngine;
using UnityEngine.AI;

public class Witch : MonoBehaviour
{
    public enum State { Idle, Chase, Attack, Dead }
    public State currentState = State.Idle;

    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private EnemyHealth enemyHealth;

    public GameObject projectilePrefab;
    public Transform firePoint;

    public float chaseRange = 15f;
    public float attackRange = 15f;
    public float attackCooldown = 3f;
    private float lastAttackTime;

    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.OnDeath += HandleDeath;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Idle:
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttacking", false);
                if (distance < chaseRange)
                {
                    ChangeState(State.Chase);
                }
                break;

            case State.Chase:
                animator.SetBool("isChasing", true);
                animator.SetBool("isAttacking", false);
                agent.isStopped = false;
                agent.SetDestination(player.position);
                if (distance < attackRange)
                {
                    ChangeState(State.Attack);
                }
                break;

            case State.Attack:
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttacking", true);
                agent.isStopped = true;
                transform.LookAt(player);

                if (distance > attackRange)
                {
                    ChangeState(State.Chase);
                }
                else if (Time.time - lastAttackTime > attackCooldown)
                {
                    lastAttackTime = Time.time;
                    StartCoroutine(ShootProjectileDelayed(0.3f));
                }
                break;
        }
    }

    void ChangeState(State newState)
    {
        if (isDead) return;
        currentState = newState;

        switch (newState)
        {
            case State.Idle:
            case State.Attack:
                agent.isStopped = true;
                break;
            case State.Chase:
                agent.isStopped = false;
                break;
        }
    }

    public void ShootProjectile()
    {
        Debug.Log("ShootProjectile()");
        if (projectilePrefab != null && firePoint != null && player != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            WitchShot shot = projectile.GetComponent<WitchShot>();
            if (shot != null)
            {
                Vector3 direction = (player.position - firePoint.position).normalized;
                shot.SetDirection(direction);
            }
        }
    }

    private System.Collections.IEnumerator ShootProjectileDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShootProjectile();
    }

    public void ResetAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    void HandleDeath()
    {
        isDead = true;
        currentState = State.Dead;

        agent.isStopped = true;
        animator.SetBool("isDead", true);
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", false);

        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 3f);
    }
}
