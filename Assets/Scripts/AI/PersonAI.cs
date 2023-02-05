using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonAI : MonoBehaviour
{
    private static readonly int EscapingAnimKey = Animator.StringToHash("Escaping");
    
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
    [SerializeField] private GameObject _hairObject;
    [SerializeField] private float _hairDestroyTime = 5f;

    [SerializeField] private State _currentState;

    private bool _hasHair = true;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _currentState = State.None;
        _perceptionAI.OnTargetFound = Escape;
        _breakFreeAI.OnReleased = Escape;
        _breakFreeAI.OnDefeated = Defeat;
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
        StartEscapeAnimation();
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

        StartEscapeAnimation();
        SetNewState(State.BreakFree);

        _breakFreeAI.Init(holder);
    }

    public void BreakFree()
    {
        if (_currentState != State.BreakFree) return;

        _breakFreeAI.HideTimer();
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
                // _breakFreeAI.Release();
                break;
            }
        }

        _currentState = newState;
    }

    public bool IsEscaping()
    {
        return _currentState == State.Escape;
    }

    private void DetachHair()
    {
        _hasHair = false;
        
        _hairObject.transform.SetParent(null, true);
        _hairObject.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(_hairObject, _hairDestroyTime);
    }

    private void Defeat()
    {
        DetachHair();
        BreakFree();
    }

    private void StartEscapeAnimation()
    {
        _animator.SetTrigger(EscapingAnimKey);
    }
}
