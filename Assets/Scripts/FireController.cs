using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour, IInteractive {
    public float burnPowerPerWood = 10;
    FireHealth _fireHealth;
    List<GameObject> _torchesAdded;

    void Awake() {
        _fireHealth = GetComponent<FireHealth>();
        _torchesAdded = new List<GameObject>();
    }

    public void Highlight() {
        Outline outlineComponent = GetComponent<Outline>();
        if (outlineComponent == null)
            outlineComponent = gameObject.AddComponent<Outline>();

        outlineComponent.OutlineMode = Outline.Mode.OutlineAll;
        outlineComponent.OutlineColor = Color.green;
        outlineComponent.OutlineWidth = .2f;
        outlineComponent.enabled = true;
    }

    public void Unhighlight() {
        Outline outlineComponent = GetComponent<Outline>();
        if (outlineComponent != null)
            outlineComponent.enabled = false;
    }

    public void Interact() {
    }

    public void AddWood() {
        _fireHealth.Add(burnPowerPerWood);
    }
    public void PickTorch() {
        _fireHealth.Remove(burnPowerPerWood);
    }
}
