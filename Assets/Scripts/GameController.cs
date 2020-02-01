using System.Collections;
using System.Collections.Generic;
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
    public IInteractive activeInteractiveElement
    {
        get { return _activeInteractiveElement; }
        set
        {
            if(_activeInteractiveElement != null && _activeInteractiveElement as MonoBehaviour != null)
                _activeInteractiveElement.Unhighlight();
            
            _activeInteractiveElement = value;
            
            if(_activeInteractiveElement != null && _activeInteractiveElement as MonoBehaviour != null)
                _activeInteractiveElement.Highlight();
        }
    }
    public static GameController instance;

    GameState _gameState;
    public GameState gameState
    {
        get { return _gameState; }
        set
        {
            _gameState = value;
            UIManager.ShowUIPanel((int)_gameState);
            if (_gameState == GameState.MainMenu
                || _gameState == GameState.Paused
                || _gameState == GameState.GameOver)
            {
                
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            
        }
    }
    void Awake()
    {
        instance = this;

       
        AddUIScene();
        
    }

    void Start()
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

    void AddUIScene()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    void LateUpdate()
    {
        //Debug.Log($"_fireHealth.HasMoreThan(0f) {!_fireHealth.HasMoreThan(0f)} playing {_gameState == GameState.Playing}");
        if (!_fireHealth.HasMoreThan(0f) && _gameState == GameState.Playing)
        {
            gameState = GameState.GameOver;
        }
    }
}
