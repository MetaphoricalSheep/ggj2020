using UnityEngine;

public class InteractComponent : MonoBehaviour
{
    [SerializeField] private bool _interact;
    
    private IInteractive _obj;

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
