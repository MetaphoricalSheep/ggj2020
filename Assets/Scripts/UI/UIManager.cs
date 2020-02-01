using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> uiPanels;

    [SerializeField] TMP_Text _timerSurviving;
    [SerializeField] TMP_Text _timerSurvivingGameOver; 
    /*
     * 0 MainMenu
     * 1 Playing
     * 2 Pause Menu
     * 3 Game Over
     */
    static UIManager instance;

    void Awake()
    {
        instance = this;
        
        if(Camera.main != null)
            Destroy(GameObject.Find("DummyCamera"));
    }

    public static void ShowUIPanel(int panelIndex)
    {
        for (int i = 0; i < instance.uiPanels.Count; i++)
            instance.uiPanels[i].SetActive(i == panelIndex);
    }

    public void OnClickPlay()
    {
        Debug.Log($"OnClickPlay!");
        GameController.instance.gameState = GameState.Playing;
    }

    public void OnClickGameOverOK()
    {
        SceneManager.LoadScene("Pieter_waldlevel");
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            //TODO implement pause
            Debug.Log($"Pause!");
        }

        _timerSurviving.text = _timerSurvivingGameOver.text= GameController.instance.secondsSurvived.ToString("0.0");
        
    }
    
}
