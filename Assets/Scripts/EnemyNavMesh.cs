using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public Transform player;
    public LayerMask layerMask;
    private NavMeshAgent agent;
    public Transform[] waypoints;
    public float detectionRange = 10f;
    public enum EnemyState
    {
        WayPatrol,
        Pursuit,
        Flee
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

            case EnemyState.Flee:
                Flee();
                break;

            default:
                break;
        }
    }

    void Pursuit()
    {
        if (player == null) return;
        agent.stoppingDistance = 6f;
        agent.SetDestination(player.position);
        transform.LookAt(player);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange)
        {
            ChangeState(EnemyState.WayPatrol);
        }
        else if (distanceToPlayer < 5f)
        {
            ChangeState(EnemyState.Flee);
        }
    }

    void WayPatrol()
    {
        agent.stoppingDistance = 0f;
        
        if (waypoints.Length != 0 && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            int randomIndex = Random.Range(0, waypoints.Length);
            agent.SetDestination(waypoints[randomIndex].position);
        }
        SearchPlayer();
    }

    void SearchPlayer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 15f, layerMask))
        {
            ChangeState(EnemyState.Pursuit);
            CallBackup();
        }
    }

    void CallBackup()
    {
        EnemyNavMesh[] todosInimigos = FindObjectsByType<EnemyNavMesh>();

        foreach (EnemyNavMesh inimigo in todosInimigos)
        {
            float distancia = Vector3.Distance(transform.position, inimigo.transform.position);
            if (inimigo != this && distancia <= detectionRange)
            {
                inimigo.ChangeState(EnemyState.Pursuit);
            }
        }
    }

    void Flee()
    {
        if (player == null) return;
        agent.stoppingDistance = 0f;
        Vector3 awayDirection = (transform.position - player.position).normalized * 10f;
        awayDirection+= transform.position;
        agent.SetDestination(awayDirection);
    }

}
