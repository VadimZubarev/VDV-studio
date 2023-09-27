using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    public float sensitivity = 450;
    public Transform _target;
    public float _distanceFromTarget = 4f;

    float xRotation;
    float yRotation;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    private float _smoothTime = 0.2f;

    private void Update()
    {
        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Vector3 nextRotation = new Vector3(xRotation, yRotation);

        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;

        transform.position = _target.position - transform.forward * _distanceFromTarget;
    }


}
