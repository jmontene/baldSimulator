using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreakFreeAI : MonoBehaviour
{
    [SerializeField] private Player _holder;
    [SerializeField] private float _angleLimit = 180f;
    [SerializeField] private float _angleSpeed = 10f;
    [SerializeField] private float _changeSignMinDelay = 3f;
    [SerializeField] private float _changeSignMaxDelay = 6f;
    [SerializeField] private float _struggleDistance = 1f;
    [SerializeField] private float _escapeTime = 5f;

    public Action OnReleased { get; set; }
    
    private Rigidbody _rb;
    private Vector3 _startDirection;
    private int _currentSign = 1;
    private float _changeSignDelay;
    private float _currentEscapeTime = 0f;
    private float _currentDefeatTime = 0f;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(Player holder)
    {
        _holder = holder;
        _startDirection = GetCurrentDirection();
        _currentEscapeTime = _escapeTime;
        _currentDefeatTime = holder.DefeatTime;

        SetChangeSignDelay();
    }

    private Vector3 GetCurrentDirection()
    {
        return (transform.position - _holder.transform.position).normalized;
    }

    private bool HolderHasCorrectInput() {
        Vector3 holderInput = _holder.GetInputVector();
        float correctSignX = 0f;
        float correctSignZ = 0f;
        float xDiff = transform.position.x - _holder.transform.position.x;
        float zDiff = transform.position.z - _holder.transform.position.z;

        if (Mathf.Abs(xDiff) > _struggleDistance) {
            correctSignX = (xDiff > 0) ? -1f : 1f; 
        }

        if (Mathf.Abs(zDiff) > _struggleDistance) {
            correctSignZ = (zDiff > 0) ? -1f : 1f;
        }

        return Mathf.Sign(holderInput.x) == correctSignX && Mathf.Sign(holderInput.z) == correctSignZ;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_holder == null) return;
        
        UpdateChangeSign();
        
        var currentDirection = GetCurrentDirection();
        var startAngle = Vector3.SignedAngle(_startDirection, currentDirection, Vector3.up);
        
        var currentAngle = startAngle + _angleSpeed * Time.deltaTime * _currentSign;
        currentAngle = Mathf.Clamp(currentAngle, _angleLimit / 2 * -1, _angleLimit / 2);

        var movement = currentAngle - startAngle;
        RotateAround(_holder.transform.position, Vector3.up, movement);
    }

    private void Update() {
        if (_holder == null) return;

        if (HolderHasCorrectInput()) {
            _currentDefeatTime -= Time.deltaTime;
            if (_currentDefeatTime <= 0) {
                Destroy(gameObject);
            }
        } else {
            _currentEscapeTime -= Time.deltaTime;
            if (_currentEscapeTime <= 0) {
                _holder.Stun();
                Release();
            }
        }
    }

    private void RotateAround(Vector3 origin, Vector3 axis, float angle)
    {
        var q = Quaternion.AngleAxis(angle, axis);
        _rb.MovePosition(q * (_rb.transform.position - origin) + origin);
        _rb.MoveRotation(_rb.transform.rotation * q);
    }

    private void UpdateChangeSign()
    {
        if (_changeSignDelay > 0f)
        {
            _changeSignDelay -= Time.deltaTime;
            return;
        }

        _currentSign *= -1;
        SetChangeSignDelay();
    }

    private void SetChangeSignDelay()
    {
        _changeSignDelay = Random.Range(_changeSignMinDelay, _changeSignMaxDelay);
    }

    public void Release()
    {
        _holder = null;
        OnReleased.Invoke();
    }
}
