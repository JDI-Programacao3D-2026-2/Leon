using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public Transform[] waypoints;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        //Pursuit();
        WayPatrol();
        //RandomPatrol();
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
