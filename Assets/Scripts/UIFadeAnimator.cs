using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFadeAnimator : MonoBehaviour
{
    [SerializeField] private AnimationCurve _fadeAnimationCurve;
    [Range(.1f,5f)]
    [SerializeField] private float _duration;
    private CanvasGroup _canvasGroup;
    public bool shown = false;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = shown ? 1 : 0;
    }

    
    public void Show(float delay = 0)
    {
        if (shown)
            return;
        shown = true;
        StopAllCoroutines();
        StartCoroutine(FadeAnimation(0, 1, _duration, delay));
        
    }
    public void Hide(float delay = 0)
    {
        if (!shown)
            return;
        
        shown = false;
        StopAllCoroutines();
        StartCoroutine(FadeAnimation(1, 0, _duration, delay));
    }


    IEnumerator FadeAnimation(float fromAlpha, float toAlpha, float durationInSec, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        float t0 = Time.unscaledTime;
        float r = 0;
        do
        {
            r = (Time.unscaledTime - t0) / durationInSec;
            _canvasGroup.alpha = Mathf.LerpUnclamped(fromAlpha, toAlpha, _fadeAnimationCurve.Evaluate(r));
            yield return null;
        } while (r < 1);
        _canvasGroup.alpha = toAlpha;
    }
}
