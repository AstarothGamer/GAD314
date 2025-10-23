using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Damageable
{
    public float detectionRange = 15f;   
    public float attackRange = 2.2f;    
    public float attackCooldown = 1.2f;  
    public int damage = 15;          
    public float rotationSpeed = 8f;

    public Animator animator; 
    public Transform player;             
    public NavMeshAgent agent;           

    private bool isAttacking = false;
    private float lastAttackTime = -99f;

    private enum State { Idle, Chasing, Attacking }
    private State currentState = State.Idle;

    void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (player == null && GameObject.FindWithTag("Player") != null)
            player = GameObject.FindWithTag("Player").transform;

        agent.stoppingDistance = attackRange - 0.1f;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Idle:
                if (distance <= detectionRange)
                    SwitchState(State.Chasing);
                break;

            case State.Chasing:
                ChasePlayer(distance);
                break;

            case State.Attacking:
                FacePlayer();
                break;
        }
    }

    private void SwitchState(State newState)
    {
        currentState = newState;
        if (animator != null)
        {
            animator.SetBool("isMoving", newState == State.Chasing);
            animator.SetBool("isAttacking", newState == State.Attacking);
        }
    }

    private void ChasePlayer(float distance)
    {
        if (distance > detectionRange * 1.5f)
        {
            agent.ResetPath();
            SwitchState(State.Idle);
            return;
        }

        if (distance <= attackRange)
        {
            agent.ResetPath();
            SwitchState(State.Attacking);
            StartCoroutine(AttackRoutine());
        }
        else
        {
            if (agent.enabled)
                agent.SetDestination(player.position);
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (Vector3.Distance(transform.position, player.position) <= attackRange + 0.2f)
        {
            FacePlayer();

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;

                if (animator != null)
                    animator.SetTrigger("Attack");

                yield return new WaitForSeconds(0.4f);

                if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange + 0.3f)
                {
                    var damageable = player.GetComponent<PlayerHealth>();
                    if (damageable != null)
                        damageable.Damage(damage);
                }
            }

            yield return null;
        }

        SwitchState(State.Chasing);
    }

    private void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        if (direction == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

