using UnityEngine;
using Random = UnityEngine.Random;

public class TreeController : MonoBehaviour, IInteractive
{
    [SerializeField] private Vector2 _woodRange = new Vector2(3, 5);
    [SerializeField] private Transform _woodTransform;

    private SoundManager _soundManager;

    private int _wood;
    Transform _transform;
    private TutorialController tutorialController;
    private static bool tutorialPlayed;

    void Awake()
    {
        _transform = GetComponent<Transform>();
        tutorialController = GameObject.FindGameObjectWithTag("TutorialController").GetComponent<TutorialController>();

    }

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

    private void Start()
    {
        _wood = Random.Range((int) _woodRange.x, (int) _woodRange.y);
        _soundManager = SoundManager.Instance;
    }

    void Update()
    {
        _transform.localRotation = Quaternion.Slerp(_transform.localRotation, Quaternion.identity, Time.deltaTime * 10f);
    }

    private void ChopTree()
    {
        if (_wood == 0)
        {
            return;
        }
        
        _soundManager.PlayChop();
        _wood--;
        _transform.localRotation = Quaternion.Euler(Random.Range(-15f,15f),0,Random.Range(-15f,15f));
            
        SpawnWood();
        Debug.Log(_wood);
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
        if (!tutorialPlayed) {
            tutorialPlayed = true;
            tutorialController.PlayReturnLog();
        }

        Destroy(gameObject);
    }
}
