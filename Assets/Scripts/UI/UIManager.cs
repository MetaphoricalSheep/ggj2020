using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> uiPanels;

    [SerializeField] TMP_Text _timerSurviving;
    [SerializeField] TMP_Text _timerSurvivingGameOver;
    
    [SerializeField] GameObject _helperPanel;
    [SerializeField] TMP_Text _helperMessage;
    private SoundManager _soundManager;
    /*
     * 0 MainMenu
     * 1 Playing
     * 2 Pause Menu
     * 3 Game Over
     */
    static UIManager instance;

    private void Awake()
    {
        instance = this;
        _soundManager = SoundManager.Instance;
        
        if(Camera.main != null)
            Destroy(GameObject.Find("DummyCamera"));
    }

    public static void ShowUIPanel(int panelIndex)
    {
        for (int i = 0; i < instance.uiPanels.Count; i++)
            instance.uiPanels[i].SetActive(i == panelIndex);

        if (GameController.instance.gameState == GameState.GameOver)
        {
            SoundManager.Instance.PlayGameOver();
        }
    }

    public static void ShowHelpMessage(string helpMessage)
    {
        instance._helperPanel.SetActive(true);
        instance._helperMessage.text = helpMessage;
    }

    public static void HideHelpMessage()
    {
        instance._helperPanel.SetActive(false);
    }
    
    public void OnClickPlay()
    {
        Debug.Log($"OnClickPlay!");
        GameController.instance.gameState = GameState.Playing;
        _soundManager.PlayGameStart();
    }
   
    public void OnClickGameOverOK()
    {
        SceneManager.LoadScene("Rene_scene2");
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            //TODO implement pause
            Debug.Log($"Pause!");
        }

        if (GameController.instance == null)
            return;
        
        if (Input.GetButtonDown("Fire1")
            || Input.GetButtonDown("Fire2")
            || Input.GetButtonDown("Fire3")   
            || Input.GetButtonDown("Jump")){
            if (GameController.instance.gameState == GameState.MainMenu)
            {
                OnClickPlay();
            }
            else if (GameController.instance.gameState == GameState.GameOver)
            {
                OnClickGameOverOK();
            }
        }

        _timerSurviving.text = _timerSurvivingGameOver.text= ((int)GameController.instance.secondsSurvived).ToString();
        
    }
    
}
