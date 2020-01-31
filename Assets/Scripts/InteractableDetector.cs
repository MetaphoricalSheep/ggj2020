using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
   
    void OnTriggerEnter(Collider other)
    {
        IInteractive interactive = other.GetComponentInParent<IInteractive>();
        if (interactive != null)
        {
            //We have detected an interactive element
            GameController.instance.activeInteractiveElement = interactive;
        }
    }

    void OnTriggerExit(Collider other)
    {
        IInteractive interactive = other.GetComponentInParent<IInteractive>();
        if (interactive != null)
        {
            GameController.instance.activeInteractiveElement = null;
        }
    }
}
