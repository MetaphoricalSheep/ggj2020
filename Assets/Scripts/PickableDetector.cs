using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableDetector : MonoBehaviour
{
    [SerializeField] CharacterHands _characterHands;
    void OnTriggerEnter(Collider other)
    {
        if (_characterHands.currentlyHolding != Holdable.Nothing)
            return;
        
        IPickable pickable = other.GetComponentInParent<IPickable>();
        
        if (pickable != null && (pickable as MonoBehaviour) != null)
        {
            pickable.Pick();
            if (other.CompareTag("Wood"))
            {
                _characterHands.SetHolding(Holdable.Wood);
            }
        }
        
    }
}
