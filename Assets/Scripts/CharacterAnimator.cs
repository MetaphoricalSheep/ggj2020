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
    [SerializeField] float runningVelocityThreshold;
    void Awake()
    {
        _transform = transform;
        _animator = GetComponent<Animator>();
        _animator.SetInteger("state",0);
    }

    void FixedUpdate()
    {
        float currentVelocity = Vector3.Distance(_transform.position, lastPosition);
        //Debug.Log($"{currentVelocity}");
        if (currentVelocity > runningVelocityThreshold)
        {
            if (_animator.GetInteger("state") != 1)
            {
                Debug.Log($"Setting run state");
                _animator.SetInteger("state", 1);
            }
        }
        else
        {
            if(_animator.GetInteger("state") != 0)
                _animator.SetInteger("state", 0);
        }
        lastPosition = _transform.position;
    }
}
