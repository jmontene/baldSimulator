using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 10f;
    
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        
    }

    private Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        // Get Random Point inside Sphere which position is center, radius is maxDistance
        var randomPos = Random.insideUnitSphere * maxDistance + center;

        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(randomPos, out var hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }

    private void SetNewDestination()
    {
        _agent.SetDestination(GetRandomPoint(_agent.transform.position, _maxDistance));
    }

    public void Wander()
    {
        _agent.isStopped = false;
        SetNewDestination();
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    private bool IsPathCompleted()
    {
        if (_agent.pathPending) return false;
        if (_agent.remainingDistance > _agent.stoppingDistance) return false;
        return !_agent.hasPath && Mathf.Approximately(_agent.velocity.sqrMagnitude, 0f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_agent.isStopped) return;
        
        if (IsPathCompleted())
        {
            SetNewDestination();
        }
    }
}
