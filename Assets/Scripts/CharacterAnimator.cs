using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator _animator;
    Transform _transform;
    Vector3 lastPosition;
    [SerializeField] CharacterHands _characterHands;
    public bool isChopping = false;
    void Awake()
    {
        _transform = transform;
        _animator = GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        
        float currentVelocity = Vector3.Distance(_transform.position, lastPosition);
        if (!isChopping)
        {
            _animator.SetFloat("velocity", currentVelocity);
            _animator.SetBool("isCarrying", _characterHands.currentlyHolding != Holdable.Nothing);
        }

        lastPosition = _transform.position;
    }

    public void ChopAnimation()
    {
        Debug.Log($"ChopAnimation");
        isChopping = true;
        _animator.SetBool("isChopping", true);
        Invoke("ChopEnded",.3f);
    }

    void ChopEnded()
    {
        _animator.SetBool("isChopping", false);
        isChopping = false;
    }
}
