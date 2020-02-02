using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public TextMeshProUGUI chopTreeText;
    public TextMeshProUGUI returnLogText;
    public TextMeshProUGUI getTorchText;
    public TextMeshProUGUI placeTorchText;
    public TextMeshProUGUI finalText;
    public Image talkingDude;

    private SoundManager _soundManager;

    public float fadeTime = 0.2f;
    public float fadeOutPixels = -1000;
    public float fadeInPixels = -150;
    public float stayDuration = 5f;

    private float animationStartTime = -10f;
    private bool _fadingOut;

    private void Start()
    {
        _soundManager = SoundManager.Instance;
        PlayChopTree();
    }

    public void PlayChopTree()
    {
        StartFadeIn();
        chopTreeText.enabled = true;
    }

    public void PlayReturnLog()
    {
        StartFadeIn();
        returnLogText.enabled = true;
    }

    public void PlayGetTorch()
    {
        StartFadeIn();
        getTorchText.enabled = true;
    }

    public void PlayPlaceTorch()
    {
        StartFadeIn();
        placeTorchText.enabled = true;
    }

    public void PlayFinalText()
    {
        StartFadeIn();
        finalText.enabled = true;
    }

    private void StartFadeIn()
    {
        _soundManager.PlayDelayed(nameof(_soundManager.PlayTreeWhooshIn), 0.1f);
        _soundManager.PlayDelayed(nameof(_soundManager.PlayTreeTalk), 0.15f);
        animationStartTime = Time.time;
        DistableTheTexts();
        FadeInTalkingDude();
    }

    private void Update()
    {
        if (Time.time - animationStartTime < fadeTime)
        {
            FadeInTalkingDude();
        }
        else if (Time.time - animationStartTime > stayDuration - fadeTime &&
                 Time.time - animationStartTime < stayDuration)
        {
            if (!_fadingOut)
            {
                _soundManager.PlayDelayed(nameof(_soundManager.PlayTreeWhooshOut), 0.1f);
            }
            
            _fadingOut = true;
            FadeOutTalkingDude();
        }
        else
        {
            _fadingOut = false;
        }
    }

    public void FadeInTalkingDude()
    {
        float newPos = Mathf.Lerp(fadeOutPixels, fadeInPixels, (Time.time - animationStartTime) / fadeTime);
        talkingDude.transform.position = new Vector3(talkingDude.transform.position.x,
            newPos, talkingDude.transform.position.z);
    }

    public void FadeOutTalkingDude()
    {
        float newPos = Mathf.Lerp(fadeInPixels, fadeOutPixels, ((Time.time - animationStartTime) - (stayDuration - fadeTime)) / fadeTime);
        talkingDude.transform.position = new Vector3(talkingDude.transform.position.x,
            newPos, talkingDude.transform.position.z);
    }

    private void DistableTheTexts()
    {
        chopTreeText.enabled = false;
        returnLogText.enabled = false;
        getTorchText.enabled = false;
        placeTorchText.enabled = false;
        finalText.enabled = false;
    }
}
