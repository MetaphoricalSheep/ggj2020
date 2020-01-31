using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision enter @ "+other.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter @ "+other.name);
    }
}
