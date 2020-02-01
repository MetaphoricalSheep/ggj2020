using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> uiPanels;
    /*
     * 0 MainMenu
     * 1 Playing
     * 2 Pause Menu
     * 3 Game Over
     */
    static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(instance);
        }
        
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
        GameController.instance.gameState = GameState.Playing;
    }

    public void OnClickGameOverOK()
    {
        SceneManager.LoadScene("Main");
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            //TODO implement pause
            Debug.Log($"Pause!");
        }
    }
    
}
