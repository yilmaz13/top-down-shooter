using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyView : AgentView
{
    [SerializeField]private NavMeshAgent _navMeshAgent;   
    public void Move(Vector3 direction)
    {
        _navMeshAgent.SetDestination(direction);
    }

    public void LookAtPlayer(Transform directionToPlayer)
    {
        transform.LookAt(directionToPlayer);
    }
}
