using UnityEngine;

public class InteractComponent : MonoBehaviour
{
    [SerializeField] private bool _interact;
    
    private IInteractive _obj;
    Transform _transform;

    void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    private void Start()
    {
        _obj = gameObject.GetComponent<IInteractive>();
    }

    private void Update()
    {
        if (!_interact)
        {
            return;
        }
    
        _interact = false;
        _obj.Interact();
    }
}
