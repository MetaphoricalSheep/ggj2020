using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _heartBeatSource;
    [SerializeField] private AudioSource _treeTalkSource;

    [SerializeField] private AudioClip[] _dropLog;
    [SerializeField] private AudioClip _chop;
    [SerializeField] private AudioClip _collectLog;
    [SerializeField] private AudioClip _placeTorch;
    [SerializeField] private AudioClip _gameStart;
    [SerializeField] private AudioClip _gameOver;
    [SerializeField] private AudioClip _treeWhooshIn;
    [SerializeField] private AudioClip _treeWhooshOut;
    
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
        _audioSource.PlayOneShot(_dropLog[Random.Range(0, _dropLog.Length)], 0.5f);
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
        _audioSource.PlayOneShot(_gameOver, 0.1f);
    }

    public void ChangeHeartBeatVolume(float volumeScale=1f)
    {
        _heartBeatSource.volume = volumeScale;
        _heartBeatSource.pitch = 1.3f + volumeScale / 2;
    }

    public void PlayTreeWhooshIn()
    {
        _audioSource.PlayOneShot(_treeWhooshIn);
    }

    public void PlayTreeWhooshOut()
    {
        _audioSource.PlayOneShot(_treeWhooshOut);
    }

    public void PlayDelayed(string soundMethod, float delay = 0.25f)
    {
        Invoke(soundMethod, delay);
    }

    public void PlayTreeTalk()
    {
        _treeTalkSource.Stop();
        _treeTalkSource.Play();
    }

    private void Awake()
    {
        Instance = this;
    }
}