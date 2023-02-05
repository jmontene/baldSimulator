using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonAI : MonoBehaviour
{
    private enum State
    {
        None,
        Wander,
        Escape,
        BreakFree
    }
    
    [SerializeField] private WanderAI _wanderAI;
    [SerializeField] private CharacterPerceptionAI _perceptionAI;
    [SerializeField] private EscapeAI _escapeAI;
    [SerializeField] private BreakFreeAI _breakFreeAI;

    [SerializeField] private State _currentState;

    private bool _hasHair = true;

    private void Start()
    {
        _currentState = State.None;
        _perceptionAI.OnTargetFound = Escape;
        _breakFreeAI.OnReleased = Escape;
        Wander();
    }

    private void Wander()
    {
        SetNewState(State.Wander);
        
        _wanderAI.Wander();
        _perceptionAI.enabled = true;
    }

    private void Escape()
    {
        SetNewState(State.Escape);
        
        _escapeAI.Escape();
    }

    private void StopWander()
    {
        _wanderAI.Stop();
        _perceptionAI.enabled = false;
    }

    public void AttemptToBreakFree(Player holder)
    {
        if (!_hasHair) return;
        
        SetNewState(State.BreakFree);

        _breakFreeAI.Init(holder);
    }

    public void BreakFree()
    {
        if (_currentState != State.BreakFree) return;
        
        Escape();
    }

    private void SetNewState(State newState)
    {
        switch (_currentState)
        {
            case State.Wander:
            {
                StopWander();
                break;
            }
            case State.Escape:
            {
                _escapeAI.Stop();
                break;
            }
            case State.BreakFree:
            {
                _breakFreeAI.Release();
                break;
            }
        }

        _currentState = newState;
    }

    public bool IsEscaping()
    {
        return _currentState == State.Escape;
    }

    public void DetachHair()
    {
        _hasHair = false;
    }
}
