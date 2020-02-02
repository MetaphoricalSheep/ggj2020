using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    [SerializeField] private Transform _torchPrefab;
    [SerializeField] CharacterAnimator _charAnimator;

    CharacterHands _characterHands;
    public float speed = 10f;
    Transform _cameraTransform;
    Transform _transform;
    Quaternion _targetRotation;
    private Rigidbody rigidbody;

    private TorchController _carriedTorch;

    private void Awake()
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

    private void UpdateInteractiveInput()
    {
        if (!InteractButtonPressed())
        {
            return;
        }

        var interactElement = GameController.instance.activeInteractiveElement as MonoBehaviour;

        if (interactElement != null)
        {
            
            if (GameController.instance.activeInteractiveElement as TreeController != null
                )
            {
                if (!_charAnimator.isChopping)
                {
                    // Cut the tree only if character ended chopping animation
                    GameController.instance.activeInteractiveElement.Interact();
                    _charAnimator.ChopAnimation();
                }
            }
            else
            {
                GameController.instance.activeInteractiveElement.Interact();

                if (_characterHands.currentlyHolding == Holdable.Wood ||
                    _characterHands.currentlyHolding == Holdable.Torch)
                {
                    if (_carriedTorch != null)
                    {
                        Destroy(_carriedTorch.gameObject);
                    }

                    _characterHands.AddWoodToFire();

                    return;
                }

                if (!interactElement.CompareTag("Fire"))
                {
                    return;
                }

                var torchFuel = _characterHands.PickTorch();

                _carriedTorch = TorchController.Craft(_torchPrefab, _transform, torchFuel);

                return;
            }
        }

        if (_carriedTorch == null)
        {
            return;
        }

        _carriedTorch.Place(_transform.position + _transform.forward);
        _carriedTorch.transform.parent = null;
        _carriedTorch.gameObject.SetActive(true);
        _carriedTorch = null;
        _characterHands.SetHolding(Holdable.Nothing);
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    void Update()
    {
        UpdateInteractiveInput();
    }

    private bool InteractButtonPressed()
    {
        return Input.GetButtonDown("Fire1")
               || Input.GetButtonDown("Fire2")
               || Input.GetButtonDown("Fire3")
               || Input.GetButtonDown("Jump");
    }
}
