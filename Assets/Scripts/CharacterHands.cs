using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Holdable {Nothing, Wood, Torch}

public class CharacterHands : MonoBehaviour
{
    [SerializeField] GameObject holdingWood;
    [SerializeField] GameObject holdingTorch;
        
    [SerializeField] Transform leftHandTransform;
    Holdable currentlyHolding; 
    public void SetHolding(Holdable newHoldable)
    {
        holdingWood.SetActive(newHoldable == Holdable.Wood);
        holdingTorch.SetActive(newHoldable == Holdable.Torch);
        currentlyHolding = newHoldable;
    }
}
