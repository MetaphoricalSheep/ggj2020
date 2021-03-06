using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}

public class GameController : MonoBehaviour
{
    IInteractive _activeInteractiveElement;
    FireHealth _fireHealth;
    public static GameController instance;
    float _secondsSurvived = 0;
    public float secondsSurvived => _secondsSurvived;

    public SoundManager SoundManager;
    
    public IInteractive activeInteractiveElement
    {
        get => _activeInteractiveElement;
        set
        {
            if (_activeInteractiveElement != null && _activeInteractiveElement as MonoBehaviour != null)
                _activeInteractiveElement.Unhighlight();

            _activeInteractiveElement = value;

            if (_activeInteractiveElement != null && _activeInteractiveElement as MonoBehaviour != null)
                _activeInteractiveElement.Highlight();
        }
    }

    GameState _gameState;
    public GameState gameState
    {
        get { return _gameState; }
        set
        {
            if (_gameState == GameState.MainMenu && value == GameState.Playing)
                _secondsSurvived = 0;
                
            _gameState = value;
            UIManager.ShowUIPanel((int)_gameState);
            if (_gameState == GameState.MainMenu
                || _gameState == GameState.Paused
                || _gameState == GameState.GameOver)
            {
                Cursor.visible = true;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.visible = false;
                Time.timeScale = 1;
            }
        }
    }

    private void Awake()
    {
        instance = this;
        AddUIScene();
    }

    private void Start()
    {
        GameObject fireGO =GameObject.FindWithTag("Fire");
        if (fireGO == null) 
        {
            Debug.LogError("Please put a fire on the scene");
            return;
        }
        _fireHealth = fireGO.GetComponent<FireHealth>(); 
        gameState = GameState.MainMenu;
    }

    private void AddUIScene()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void LateUpdate()
    {
        if (gameState == GameState.Playing)
        {
            _secondsSurvived += Time.deltaTime;
        }
    }
}
