using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonAI : MonoBehaviour
{
    [SerializeField] private WanderAI _wanderAI;
    [SerializeField] private CharacterPerceptionAI _perceptionAI;
    [SerializeField] private EscapeAI _escapeAI;
    
    private void Start()
    {
        _perceptionAI.OnTargetFound = Escape;
        Wander();
    }

    private void Wander()
    {
        _wanderAI.Wander();
        _perceptionAI.enabled = true;
    }

    private void Escape()
    {
        _wanderAI.Stop();
        _perceptionAI.enabled = false;
        _escapeAI.Escape();
    }
}
