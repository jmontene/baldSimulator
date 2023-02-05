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
    [SerializeField] private TimerUI _timerUI;

    public Action OnReleased { get; set; }
    
    private Rigidbody _rb;
    private Vector3 _startDirection;
    private int _currentSign = 1;
    private float _changeSignDelay;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(Player holder)
    {
        _timerUI.Show();
        _holder = holder;
        _startDirection = GetCurrentDirection();
        SetChangeSignDelay();
    }

    private Vector3 GetCurrentDirection()
    {
        return (transform.position - _holder.transform.position).normalized;
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
        _timerUI.Hide();
    }
}
