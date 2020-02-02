using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {
    public TMPro.TextMeshProUGUI chopTreeText;
    public TMPro.TextMeshProUGUI returnLogText;
    public TMPro.TextMeshProUGUI getTorchText;
    public TMPro.TextMeshProUGUI placeTorchText;
    public TMPro.TextMeshProUGUI finalText;
    public Image talkingDude;

    public float fadeTime = 0.2f;
    public float fadeOutPixels = -1000;
    public float fadeInPixels = -150;
    public float stayDuration = 5f;

    private float animationStartTime = -10f;

    private void Start() {
        PlayChopTree();
    }

    public void PlayChopTree() {
        StartFadeIn();
        chopTreeText.enabled = true;
    }

    public void PlayReturnLog() {
        StartFadeIn();
        returnLogText.enabled = true;
    }

    public void PlayGetTorch() {
        StartFadeIn();
        getTorchText.enabled = true;
    }

    public void PlayPlaceTorch() {
        StartFadeIn();
        placeTorchText.enabled = true;
    }

    public void PlayFinalText() {
        StartFadeIn();
        finalText.enabled = true;
    }

    public void StartFadeIn() {
        animationStartTime = Time.time;
        DistableTheTexts();
        FadeInTalkingDude();
    }

    private void Update() {
        if (Time.time - animationStartTime < fadeTime) {
            FadeInTalkingDude();
        } else if (Time.time - animationStartTime > stayDuration - fadeTime &&
            Time.time - animationStartTime < stayDuration) {
            FadeOutTalkingDude();
        }
    }

    public void FadeInTalkingDude() {
        float newPos = Mathf.Lerp(fadeOutPixels, fadeInPixels, (Time.time - animationStartTime) / fadeTime);
        talkingDude.transform.position = new Vector3 (talkingDude.transform.position.x,
            newPos, talkingDude.transform.position.z);
    }

    public void FadeOutTalkingDude() {
        float newPos = Mathf.Lerp(fadeInPixels, fadeOutPixels, ((Time.time - animationStartTime) - (stayDuration - fadeTime)) / fadeTime);
        talkingDude.transform.position = new Vector3(talkingDude.transform.position.x,
            newPos, talkingDude.transform.position.z);
    }

    private void DistableTheTexts() {
        chopTreeText.enabled = false;
        returnLogText.enabled = false;
        getTorchText.enabled = false;
        placeTorchText.enabled = false;
        finalText.enabled = false;
    }
}
