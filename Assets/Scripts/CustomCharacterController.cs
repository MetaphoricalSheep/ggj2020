using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    CharacterHands _characterHands;
    public float speed = 10f;
    Transform _cameraTransform;
    Transform _transform;
    Quaternion _targetRotation;
    private Rigidbody rigidbody;

    void Awake()
    {
        if (Camera.main != null) _cameraTransform = Camera.main.GetComponent<Transform>();
        _transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        _characterHands = GetComponent<CharacterHands>();
    }

    void UpdateMovement()
    {
        Vector3 forwardDirection = new Vector3(_cameraTransform.forward.x, 0, _cameraTransform.forward.z).normalized;
        Vector3 rightDirection = (Quaternion.Euler(0f, 90f, 0f) * forwardDirection).normalized;
        Vector3 lookingDirection = Vector3.zero;

        lookingDirection += forwardDirection * Input.GetAxis("Vertical");
        lookingDirection += rightDirection * Input.GetAxis("Horizontal");

        if (lookingDirection != Vector3.zero)
        {
            lookingDirection = lookingDirection.normalized;
            _targetRotation = Quaternion.LookRotation(lookingDirection);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, _targetRotation, Time.deltaTime * 16f);
        }

        rigidbody.velocity = lookingDirection * speed;
        // _transform.position += lookingDirection * speed * Time.deltaTime;
    }

    void UpdateInteractiveInput()
    {
        if (Input.GetButtonDown("Fire1")
            || Input.GetButtonDown("Fire2")
            || Input.GetButtonDown("Fire3")
            || Input.GetButtonDown("Jump"))
        {
            if (GameController.instance.activeInteractiveElement != null && GameController.instance.activeInteractiveElement as MonoBehaviour != null)
            {
                GameController.instance.activeInteractiveElement.Interact();
                if (_characterHands.currentlyHolding == Holdable.Wood
                    || _characterHands.currentlyHolding == Holdable.Torch)
                {
                    _characterHands.AddWoodToFire();
                } 
                else 
                {
                    _characterHands.PickTorch();
                }
            }
            
        }
    }

    private void FixedUpdate()
    {
        UpdateMovement();
        
    }

    void Update()
    {
        UpdateInteractiveInput();
    }
}
