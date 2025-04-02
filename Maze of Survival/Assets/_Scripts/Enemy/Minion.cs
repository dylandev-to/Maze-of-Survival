using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Minion : MonoBehaviour
{
    public enum State { Idle, Chase, Attack, Dead }
    public State currentState = State.Idle;

    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private EnemyHealth enemyHealth;

    public float chaseRange = 10f;
    public float attackRange = 2.5f;
    public float attackCooldown = 2.5f;
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
        /*PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(10);
        }*/
    }

    void HandleDeath()
    {
        isDead = true;
        currentState = State.Dead;
        agent.isStopped = true;
        animator.SetBool("isDead", true);
        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, 3f);
    }
}
