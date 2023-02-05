using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentHandler : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Action _onDestinationReached;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_agent.enabled) return;
        
        if (IsPathCompleted())
        {
            _onDestinationReached?.Invoke();
        }
    }
    
    public void SetNewDestination(Vector3 position, Action onDestinationReached = null)
    {
        _agent.enabled = true;
        _agent.SetDestination(position);
        _onDestinationReached = onDestinationReached;
    }
    
    public void Stop()
    {
        _agent.enabled = false;
    }

    private bool IsPathCompleted()
    {
        if (_agent.pathPending) return false;
        if (_agent.remainingDistance > _agent.stoppingDistance) return false;
        return !_agent.hasPath && Mathf.Approximately(_agent.velocity.sqrMagnitude, 0f);
    }

    public void SetSpeed(float speed)
    {
        _agent.speed = speed;
    }
}
