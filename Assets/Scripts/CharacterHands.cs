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

    public void SetHolding(Holdable newHoldable)
    {
        holdingWood.SetActive(newHoldable == Holdable.Wood);
        holdingTorch.SetActive(newHoldable == Holdable.Torch);
        _currentlyHolding = newHoldable;
    }
}
