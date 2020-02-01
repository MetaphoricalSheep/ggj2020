using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Holdable {Nothing, Wood, Torch}

public class CharacterHands : MonoBehaviour
{
    [SerializeField] GameObject holdingWood;
    [SerializeField] GameObject holdingTorch;
    Holdable _currentlyHolding = Holdable.Nothing;
    public Holdable currentlyHolding => _currentlyHolding;

    FireController _fireController;

    void Awake()
    {
        GameObject fireObject = GameObject.FindWithTag("Fire");
        if (fireObject == null)
        {
            Debug.LogError("Please put a fire in the scene :3");
            return;
        }
        
        _fireController = GameObject.FindWithTag("Fire").GetComponent<FireController>();
    }

    public void SetHolding(Holdable newHoldable)
    {
        holdingWood.SetActive(newHoldable == Holdable.Wood);
        holdingTorch.SetActive(newHoldable == Holdable.Torch);
        _currentlyHolding = newHoldable;
    }

    public void AddWoodToFire()
    {
        Debug.Log($"AddWoodToFire");
        SetHolding(Holdable.Nothing);
        _fireController.AddWood();
    }

    public float PickTorch()
    {
        Debug.Log($"PickTorch");
        SetHolding(Holdable.Torch);
        _fireController.PickTorch();

        return _fireController.burnPowerPerWood;
    }
}
