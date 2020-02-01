﻿using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private AudioClip[] _dropLog;
    [SerializeField] private AudioClip _chop;
    [SerializeField] private AudioClip _collectLog;
    [SerializeField] private AudioClip _placeTorch;
    [SerializeField] private AudioClip _gameStart;
    [SerializeField] private AudioClip _gameOver;
    
    public static SoundManager Instance;

    public void PlayChop()
    {
        _audioSource.PlayOneShot(_chop);
    }

    public void PlayCollectLog()
    {
        _audioSource.PlayOneShot(_collectLog);
    }

    public void PlayDropLog()
    {
        _audioSource.PlayOneShot(_dropLog[Random.Range(0, _dropLog.Length)]);
    }

    public void PlayPlaceTorch()
    {
        _audioSource.PlayOneShot(_placeTorch);
    }

    public void PlayGameStart()
    {
        _audioSource.PlayOneShot(_gameStart, 0.5f);
    }

    public void PlayGameOver()
    {
        _audioSource.PlayOneShot(_gameOver, 0.5f);
    }

    private void Awake()
    {
        Instance = this;
    }
}