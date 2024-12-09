using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyView : AgentView
{
    public void Move(Vector3 direction)
    {
        _rigidbody.MovePosition(transform.position + direction * Time.fixedDeltaTime * _speed);
    }

    public void LookAtPlayer(Transform directionToPlayer)
    {
        transform.LookAt(directionToPlayer);
    }
}
