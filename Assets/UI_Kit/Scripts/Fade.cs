using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] Image fadeImg;

    [Header("Events")]
    [SerializeField] UnityEvent fadeSequenceComplete;
    [SerializeField] UnityEvent fadeInFinish;
    [SerializeField] UnityEvent fadeOutFinish;

    [Header("Settings")]
    [SerializeField] float timeToFade = 0.25f;
    [SerializeField] float fadeUpperValue = 1;
    [SerializeField] float fadeLowerValue = 0;

    public float TimeToFade => timeToFade;

    // Test methods
    public void FadeSequence()
    {
        StartCoroutine(FadeSequenceRoutine());
    }

    IEnumerator FadeSequenceRoutine()
    {
        yield return StartCoroutine(FadeOutRoutine(timeToFade));
        yield return StartCoroutine(FadeInRoutine(timeToFade));
        fadeSequenceComplete?.Invoke();
    }
    // Main methods
    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine(timeToFade));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine(timeToFade));
    }

    public IEnumerator FadeOutRoutine(float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            yield return new WaitForEndOfFrame();

            timeElapsed += Time.deltaTime;

            float alphaValue = timeElapsed / duration;

            Color newCol = fadeImg.color;
            newCol.a = Mathf.Lerp(fadeLowerValue, fadeUpperValue, alphaValue);

            fadeImg.color = newCol;

        }

        Color finalCol = fadeImg.color;
        finalCol.a = fadeUpperValue;

        fadeImg.color = finalCol;

        fadeOutFinish?.Invoke();
    }

    public IEnumerator FadeInRoutine(float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            yield return new WaitForEndOfFrame();

            timeElapsed += Time.deltaTime;

            float alphaValue = timeElapsed / duration;

            Color newCol = fadeImg.color;
            newCol.a = fadeUpperValue - Mathf.Lerp(fadeLowerValue, fadeUpperValue, alphaValue);

            fadeImg.color = newCol;

        }

        Color finalCol = fadeImg.color;
        finalCol.a = fadeLowerValue;

        fadeImg.color = finalCol;

        fadeInFinish?.Invoke();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FadeInRoutine(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
