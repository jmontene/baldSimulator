using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float _speed = 20.0f;
    [SerializeField] private float _rotationSpeed = 1f;

    private Rigidbody _rb;
    private Camera _camera;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        var inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        var movementDirection = _camera.transform.TransformDirection(inputVector);
        movementDirection.y = 0f;
        movementDirection.Normalize();
        
        _rb.velocity = movementDirection * _speed;

        if (inputVector != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
