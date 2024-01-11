using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
     public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float patrolWaitTime = 2f;
    public float sightRange = 10f;
    public float avoidPlayerDistance = 1.5f;
    public float resumeChasingDistance = 3f; 

    private NavMeshAgent navMeshAgent;
    private int currentPatrolIndex;
    private float patrolTimer;
    private Transform player;

    private enum AIState
    {
        Patrol,
        Chase,
        Search
    }

    private AIState currentState;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = AIState.Patrol;
        SetPatrolDestination();
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Patrol:
                Patrol();
                break;
            case AIState.Chase:
                Chase();
                break;
            case AIState.Search:
                Search();
                break;
        }

        if (CanSeePlayer())
        {
            currentState = AIState.Chase;
        }
        else if (currentState == AIState.Chase && Vector3.Distance(transform.position, player.position) < avoidPlayerDistance)
        {
            currentState = AIState.Search;
        }
        else if (currentState == AIState.Search && Vector3.Distance(transform.position, player.position) > resumeChasingDistance)
        {
            currentState = AIState.Chase;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    void Patrol()
    {
        if (navMeshAgent.remainingDistance < 0.5f)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolWaitTime)
            {
                SetPatrolDestination();
                patrolTimer = 0f;
            }
        }
    }

    void SetPatrolDestination()
    {
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        navMeshAgent.speed = patrolSpeed;
    }

    void Chase()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > avoidPlayerDistance)
        {
            navMeshAgent.SetDestination(player.position);
            navMeshAgent.speed = chaseSpeed;
            
        }
        else
        {
            stopmoving();
            navMeshAgent.ResetPath();
        }
    }

    void stopmoving()
    {
    }
    void Search()
    {
    }

    bool CanSeePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= sightRange;
    }
}
