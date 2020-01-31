using UnityEngine;
using Random = UnityEngine.Random;

public class TreeController : MonoBehaviour, IInteractive
{
    [SerializeField] private Vector2 _woodRange = new Vector2(3, 5);
    [SerializeField] private Transform _woodTransform;

    private int _wood;

    public void Highlight()
    {
        Debug.Log("Highlighting");
    }

    public void Interact()
    {
        ChopTree();
        
        if (_wood <= 0)
        {
            KillTree();
        }
    }

    private void Awake()
    {
        _wood = Random.Range((int) _woodRange.x, (int) _woodRange.y);
    }

    private void ChopTree()
    {
        if (_wood == 0)
        {
            return;
        }
        
        _wood--;
        SpawnWood();
        Debug.Log(_wood);
    }

    private void SpawnWood()
    {
        Instantiate(_woodTransform);
    }

    private void KillTree()
    {
        Destroy(gameObject);
    }
}
