using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{

    public float speed = 10f;
    Transform _cameraTransform;
    Transform _transform;
    Quaternion _targetRotation;
    void Awake()
    {
        if (Camera.main != null) _cameraTransform = Camera.main.GetComponent<Transform>();
        _transform = GetComponent<Transform>();
    }

    void UpdateMovement()
    {
        Vector3 forwardDirection = new Vector3(_cameraTransform.forward.x, 0, _cameraTransform.forward.z).normalized;
        Vector3 rightDirection = (Quaternion.Euler(0f, 90f, 0f) * forwardDirection).normalized;
        Vector3 lookingDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            lookingDirection += forwardDirection;
        else if (Input.GetKey(KeyCode.S))
            lookingDirection -= forwardDirection;

        if (Input.GetKey(KeyCode.D))
            lookingDirection += rightDirection;
        else if (Input.GetKey(KeyCode.A))
            lookingDirection -= rightDirection;

        if (lookingDirection != Vector3.zero)
        {
            lookingDirection = lookingDirection.normalized;
            _targetRotation = Quaternion.LookRotation(lookingDirection);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, _targetRotation, Time.deltaTime*16f);
        }

        _transform.position += lookingDirection * speed * Time.deltaTime;
    }

    void UpdateInteractiveInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameController.instance.activeInteractiveElement != null && GameController.instance.activeInteractiveElement as MonoBehaviour != null)
            {
                GameController.instance.activeInteractiveElement.Interact();
            }
        }
    }

    void Update()
    {
        UpdateMovement();
        UpdateInteractiveInput();
    }
}
