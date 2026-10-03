using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public Transform player;
    public LayerMask layerMask;
    private NavMeshAgent agent;
    public Transform[] waypoints;
    public enum EnemyState
    {
        WayPatrol,
        RandomPatrol,
        Pursuit
    }

    public EnemyState currentState = EnemyState.WayPatrol;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        FiniteStateMachine();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    void FiniteStateMachine()
    {
        switch (currentState)
        {
            case EnemyState.Pursuit:
                Pursuit();
                break;
        
            case EnemyState.WayPatrol:
                WayPatrol();
                break;

            default:
                break;
        }

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 15f, layerMask))
        {
            ChangeState(EnemyState.Pursuit);
        }
        else
        {
            ChangeState(EnemyState.WayPatrol);
        }
    }

    void Pursuit()
    {
        if (player == null) return;
        agent.stoppingDistance = 6f;
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    void WayPatrol()
    {
        agent.stoppingDistance = 0f;
        
        if (waypoints.Length != 0 && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            int randomIndex = Random.Range(0, waypoints.Length);
            agent.SetDestination(waypoints[randomIndex].position);
        }
    }

}
