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
    [SerializeField] GameObject _highlightObject;
    void Awake()
    {
        _fireHealth = GetComponent<FireHealth>();
        _torchesAdded = new List<GameObject>();
        tutorialController = GameObject.FindGameObjectWithTag("TutorialController").GetComponent<TutorialController>();
    }

    public void Highlight()
    {
        _highlightObject.SetActive(true);
    }

    public void Unhighlight()
    {
        _highlightObject.SetActive(false);
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
