using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 20.0f;

    private Rigidbody rb;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update() {
        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = (Vector3.forward * inputVector.y + Vector3.right * inputVector.x) * speed;

        if (inputVector != Vector2.zero) {
            transform.rotation = Quaternion.LookRotation(new Vector3(inputVector.x, 0, inputVector.y));
        }
    }
}
