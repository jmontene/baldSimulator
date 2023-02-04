using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EscapeAI : MonoBehaviour
{
    [SerializeField] private float _escapeSpeed = 8f;

    private NavMeshAgentHandler _agent;
    private EscapeRouter _escapeRouter;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgentHandler>();
        _escapeRouter = FindObjectOfType<EscapeRouter>();
    }
    
    public void Escape()
    {
        if (!_escapeRouter.TryGetClosestEscapePoint(_agent.transform.position, out var escapePoint)) return;
        
        _agent.SetSpeed(_escapeSpeed);
        _agent.SetNewDestination(escapePoint, OnEscapePointReached);
    }

    private void OnEscapePointReached()
    {
        Destroy(gameObject);
    }
}
