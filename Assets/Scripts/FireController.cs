using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class FireController : MonoBehaviour, IInteractive
{
    public float burnPowerPerWood = 10;
    FireHealth _fireHealth;
    List<GameObject> _torchesAdded;
    private bool firstWoodAdded;
    private bool firstTorchPicked;
    private TutorialController tutorialController;

    void Awake()
    {
        _fireHealth = GetComponent<FireHealth>();
        _torchesAdded = new List<GameObject>();
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
    }

    public void AddWood() {
        if (!firstWoodAdded && !firstTorchPicked) {
            firstWoodAdded = true;
            tutorialController.PlayGetTorch();
        }
        _fireHealth.Add(burnPowerPerWood);
    }
    public void PickTorch() {
        if (!firstTorchPicked) {
            firstTorchPicked = true;
            tutorialController.PlayPlaceTorch();
        }

        _fireHealth.Remove(burnPowerPerWood);
    }
}
