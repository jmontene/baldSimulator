using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 10f;
    
    private NavMeshAgentHandler _agent;

    // Start is called before the first frame update
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgentHandler>();
    }

    private static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        // Get Random Point inside Sphere which position is center, radius is maxDistance
        var randomPos = Random.insideUnitSphere * maxDistance + center;

        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(randomPos, out var hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }

    public void Wander()
    {
        _agent.SetNewDestination(GetRandomPoint(_agent.transform.position, _maxDistance), Wander);
    }

    public void Stop()
    {
        _agent.Stop();
    }
}
