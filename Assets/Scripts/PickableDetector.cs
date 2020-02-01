using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableDetector : MonoBehaviour
{
    [SerializeField] CharacterHands _characterHands;
    void OnTriggerEnter(Collider other)
    {
        IPickable pickable = other.GetComponentInParent<IPickable>();
        if (pickable != null)
        {
            if (pickable != null && (pickable as MonoBehaviour) != null)
            {
                pickable.Pick();
                
            }
        }
    }
}
