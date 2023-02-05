using System;
using UnityEngine;
using Rewired;

public class Player : MonoBehaviour
{
    private static readonly int SpeedAnimKey = Animator.StringToHash("Speed");
    private static readonly int GrabbingAnimKey = Animator.StringToHash("Grabbing");
    
    [SerializeField] private int _inputPlayerId = 0;
    [SerializeField] private float _speed = 20.0f;
    [SerializeField] private float _rotationSpeed = 1f;

    [Header("Grab Raycast")]
    [SerializeField] private float _raycastRadius = 1f;
    [SerializeField] private float _raycastDistance = 1f;
    [SerializeField] private LayerMask _raycastLayer;

    private Rigidbody _rb;
    private Animator _animator;
    private Camera _camera;
    private Rewired.Player _inputPlayer;
    private bool _isGrabbing;
    private PersonAI _grabbedPerson;
    private Collider[] _colliders;

    private void Awake()
    {
        _colliders = new Collider[10];
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _camera = Camera.main;
        _inputPlayer = ReInput.players.GetPlayer(_inputPlayerId);
    }

    // Update is called once per frame
    private void Update()
    {
        CheckGrab();
        UpdateMovement();
    }

    private void CheckGrab()
    {
        var grab = Mathf.CeilToInt(_inputPlayer.GetAxis("Primary")) == 1;

        if (grab && !_isGrabbing)
        {
            Grab();
        }
        else if (!grab && _isGrabbing)
        {
            ReleaseGrab();
        }
    }

    private void Grab()
    {
        _isGrabbing = true;
        _animator.SetBool(GrabbingAnimKey, true);
        if (AttemptToGrab(out var person))
        {
            _grabbedPerson = person;
            _grabbedPerson.AttemptToBreakFree(this);
        }
    }

    private void ReleaseGrab()
    {
        _isGrabbing = false;
        _animator.SetBool(GrabbingAnimKey, false);
        if (_grabbedPerson != null)
        {
            _grabbedPerson.BreakFree();
            _grabbedPerson = null;
        }
    }

    private bool AttemptToGrab(out PersonAI person)
    {
        person = null;
        var currentTransform = transform;

        var origin = currentTransform.position + _raycastDistance * currentTransform.forward;

        var size = Physics.OverlapSphereNonAlloc(origin, _raycastRadius, _colliders, _raycastLayer.value);

        if (size == 0) return false;

        for (var i = 0; i < size; i++)
        {
            person = _colliders[i].GetComponent<PersonAI>();
            if (person != null) return true;
        }

        return false;
    }

    private bool CanMove()
    {
        return !_isGrabbing;
    }

    private void UpdateMovement()
    {
        if (!CanMove())
        {
            _rb.velocity = Vector3.zero;
            return;
        }
        
        var movementDirection = GetMovementDirection();
        _rb.velocity = movementDirection * _speed;
        _animator.SetFloat(SpeedAnimKey, _rb.velocity.sqrMagnitude);

        if (movementDirection == Vector3.zero) return;
        
        var targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    public Vector3 GetMovementDirection()
    {
        var inputVector = new Vector3(_inputPlayer.GetAxisRaw("Horizontal"), 0f, _inputPlayer.GetAxisRaw("Vertical"));
        
        var movementDirection = _camera.transform.TransformDirection(inputVector);
        movementDirection.y = 0f;
        movementDirection.Normalize();

        return movementDirection;
    }
}
