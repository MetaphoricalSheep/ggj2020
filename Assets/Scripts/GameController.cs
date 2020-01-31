﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    IInteractive _activeInteractiveElement;
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

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }
        
    }
}