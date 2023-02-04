using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _mainCamera;
    
    // Start is called before the first frame update
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        var lookRef = _mainCamera.transform.position - transform.position;
        var lookRot = Quaternion.LookRotation(lookRef, Vector3.up);
        var currentRotation = transform.rotation.eulerAngles;
        currentRotation.y = lookRot.eulerAngles.y;
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
