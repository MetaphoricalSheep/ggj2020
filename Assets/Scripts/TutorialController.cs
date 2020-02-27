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
    private float fadeOutPixels;
    private float fadeInPixels = 0;
    public float stayDuration = 5f;
    public AnimationCurve showAnimationCurve;
    private float animationStartTime = -10f;
    private bool _fadingOut;

    private void Start()
    {
        _soundManager = SoundManager.Instance;
        fadeOutPixels = -Screen.height - fadeInPixels;
        PlayChopTree();
    }

    public void PlayChopTree()
    {
        StartFadeIn();
        chopTreeText.enabled = true;
        StopAllCoroutines();
        StartCoroutine(SpeakAnimated(chopTreeText));
    }

    public void PlayReturnLog()
    {
        StartFadeIn();
        returnLogText.enabled = true;
        StopAllCoroutines();
        StartCoroutine(SpeakAnimated(returnLogText));
    }

    public void PlayGetTorch()
    {
        StartFadeIn();
        getTorchText.enabled = true;
        StopAllCoroutines();
        StartCoroutine(SpeakAnimated(getTorchText));
    }

    public void PlayPlaceTorch()
    {
        StartFadeIn();
        placeTorchText.enabled = true;
        StopAllCoroutines();
        StartCoroutine(SpeakAnimated(placeTorchText));
    }

    public void PlayFinalText()
    {
        StartFadeIn();
        finalText.enabled = true;
        StopAllCoroutines();
        StartCoroutine(SpeakAnimated(finalText));
    }

    private void StartFadeIn()
    {
        _soundManager.PlayDelayed(nameof(_soundManager.PlayTreeWhooshIn), 0.1f);
        _soundManager.PlayDelayed(nameof(_soundManager.PlayTreeTalk), 0.15f);
        animationStartTime = Time.time;
        DistableTheTexts();
        FadeInTalkingDude();
    }

    IEnumerator SpeakAnimated(TextMeshProUGUI text)
    {
        string textToShow = text.text;
        text.text = "";
        for (int i = 0; i <= textToShow.Length; i++)
        {
            text.text = textToShow.Substring(0, i);
            yield return new WaitForSeconds(3f/60f);    
        }
        
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
        float r = (Time.time - animationStartTime) / fadeTime;
        float newPos = Mathf.LerpUnclamped(fadeOutPixels, fadeInPixels, showAnimationCurve.Evaluate(r));
        talkingDude.transform.position = new Vector3(talkingDude.transform.position.x,
            newPos, talkingDude.transform.position.z);
    }

    public void FadeOutTalkingDude()
    {
        float r = ((Time.time - animationStartTime) - (stayDuration - fadeTime)) / fadeTime;
        float newPos = Mathf.LerpUnclamped(fadeOutPixels, fadeInPixels, showAnimationCurve.Evaluate(1-r));
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
