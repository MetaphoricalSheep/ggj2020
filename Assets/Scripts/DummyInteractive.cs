using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class DummyInteractive : MonoBehaviour, IInteractive
{
    
    public void Highlight()
    {
//        Outline outlineComponent = GetComponent<Outline>();
//        if (outlineComponent == null)
//            outlineComponent = gameObject.AddComponent<Outline>();
//
//        outlineComponent.OutlineMode = Outline.Mode.OutlineAll;
//        outlineComponent.OutlineColor = Color.green;
//        outlineComponent.OutlineWidth = .2f;
//        outlineComponent.enabled = true;
    }

    public void Unhighlight()
    {
//        Outline outlineComponent = GetComponent<Outline>();
//        if (outlineComponent != null)
//            outlineComponent.enabled = false;
    }

    public void Interact()
    {
        transform.localScale = Vector3.one*2.0f;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 10f);
    }
}
