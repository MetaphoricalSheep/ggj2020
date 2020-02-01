using UnityEngine;
using Random = UnityEngine.Random;

public class TreeController : MonoBehaviour, IInteractive
{
    [SerializeField] private Vector2 _woodRange = new Vector2(3, 5);
    [SerializeField] private Transform _woodTransform;

    private int _wood;

    public void Highlight()
    {
        Outline outlineComponent = GetComponent<Outline>();
        if (outlineComponent == null)
            outlineComponent = gameObject.AddComponent<Outline>();

        outlineComponent.OutlineMode = Outline.Mode.OutlineAll;
        outlineComponent.OutlineColor = Color.green;
        outlineComponent.OutlineWidth = .2f;
        outlineComponent.enabled = true;
    }

    public void Unhighlight()
    {
        Outline outlineComponent = GetComponent<Outline>();
        if (outlineComponent != null)
            outlineComponent.enabled = false;
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
    }

    private void SpawnWood()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 2f;
        Transform wood = Instantiate(_woodTransform);
        wood.position = spawnPosition;
    }

    private void KillTree()
    {
        Destroy(gameObject);
    }
}
