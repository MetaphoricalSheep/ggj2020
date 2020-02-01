using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class CharacterAnimator : MonoBehaviour
{
    Animator _animator;
    Transform _transform;
    Vector3 lastPosition;
    [SerializeField] CharacterHands _characterHands;
    public bool isChopping = false;
    [SerializeField] AudioClip[] _stepSounds;
    AudioSource _audioSource;
    void Awake()
    {
        _transform = transform;
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
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

    public void Step()
    {
        _audioSource.pitch = Random.Range(.8f, 1.2f);
        _audioSource.PlayOneShot(_stepSounds[Random.Range(0,_stepSounds.Length)]);
    }
}
