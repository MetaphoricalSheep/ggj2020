using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class FireController : MonoBehaviour, IInteractive
{
    public float burnPowerPerWood = 10;
    FireHealth _fireHealth;
    List<GameObject> _torchesAdded;
    private bool addedWood;
    private bool pickedTorch;
    public GameObject pickWoodTutorialText;
    public GameObject pickedTorchTutorialText;
    private GameObject pickedWoodTutorialTextInstance;

    void Awake()
    {
        _fireHealth = GetComponent<FireHealth>();
        _torchesAdded = new List<GameObject>();
        RectTransform t;
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

    public void AddWood()
    {
        if (!addedWood)
        {
            addedWood = true;
            pickedWoodTutorialTextInstance = Instantiate(pickWoodTutorialText);
            pickedWoodTutorialTextInstance.transform.position = this.transform.position;
        }

        _fireHealth.Add(burnPowerPerWood);
    }

    public void PickTorch()
    {
        if (!pickedTorch)
        {
            pickedTorch = true;
            
            if (pickedWoodTutorialTextInstance != null)
            {
                Destroy(pickedWoodTutorialTextInstance);
                pickedWoodTutorialTextInstance = null;
            }

            GameObject tt = Instantiate(pickedTorchTutorialText);
            tt.transform.position = this.transform.position;
            Destroy(tt, 5f);
        }

        _fireHealth.Remove(burnPowerPerWood);
    }
}
