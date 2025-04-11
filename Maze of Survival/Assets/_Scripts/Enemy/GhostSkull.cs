using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GhostSkull : MonoBehaviour
{
    public enum State { Idle, Chase, Attack, Dead }
    public State currentState = State.Idle;

    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private EnemyHealth enemyHealth;

    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 10;
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
        if (isDead || enemyHealth == null || player == null) return;

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
                if (distance > attackRange)
                {
                    ChangeState(State.Chase);
                }
                else if (Time.time - lastAttackTime > attackCooldown)
                {
                    lastAttackTime = Time.time;
                    AttackPlayer();
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
                agent.isStopped = true;
                break;

            case State.Chase:
                agent.isStopped = false;
                break;

            case State.Attack:
                agent.isStopped = true;
                break;
        }
    }

    public void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void HandleDeath()
    {
        isDead = true;
        currentState = State.Dead;
        agent.isStopped = true;
        animator.SetBool("isDead", true);
        GetComponent<Collider>().enabled = false;

        StartCoroutine(LowerToGround());

        Destroy(gameObject, 3f);
    }

    IEnumerator LowerToGround()
    {
        float duration = 0.6f;
        float elapsedTime = 0f;
        float startOffset = agent.baseOffset;
        float targetOffset = 0f;

        while (elapsedTime < duration)
        {
            agent.baseOffset = Mathf.Lerp(startOffset, targetOffset, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        agent.baseOffset = targetOffset;
    }
}
