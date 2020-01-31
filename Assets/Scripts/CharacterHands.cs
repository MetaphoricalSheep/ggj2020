using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Holdable {Nothing, Wood, Torch}

public class CharacterHands : MonoBehaviour
{
    public Holdable currentlyHolding; 
    public void Hold(Holdable newHoldable)
    {
        currentlyHolding = newHoldable;
    }
}
