using UnityEngine;
using Rewired;

public class Player : MonoBehaviour
{
    [SerializeField] private int _inputPlayerId = 0;
    [SerializeField] private float _speed = 20.0f;
    [SerializeField] private float _rotationSpeed = 1f;

    private Rigidbody _rb;
    private Camera _camera;
    private Rewired.Player _inputPlayer;
    private bool _isGrabbing;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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
            _isGrabbing = true;
        }
        else if (!grab && _isGrabbing)
        {
            _isGrabbing = false;
        }
    }

    private bool CanMove()
    {
        return !_isGrabbing;
    }

    private void UpdateMovement()
    {
        if (!CanMove())
        {
            _rb.velocity = Vector2.zero;
            return;
        }
        
        var inputVector = new Vector3(_inputPlayer.GetAxisRaw("Horizontal"), 0f, _inputPlayer.GetAxisRaw("Vertical"));

        var movementDirection = _camera.transform.TransformDirection(inputVector);
        movementDirection.y = 0f;
        movementDirection.Normalize();
        
        _rb.velocity = movementDirection * _speed;

        if (inputVector == Vector3.zero) return;
        
        var targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}
