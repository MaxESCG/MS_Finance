using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ProgressBar progressBar;
    [SerializeField] TextMeshProUGUI textTimer;

    [Header("Events")]
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    [Header("Settings")]
    [SerializeField] string textPrefix = "Time Remaining: ";
    [SerializeField] string textSuffix = "";
    [SerializeField] string timeFinishedText = "Out of Time";
    [SerializeField] int decimalToRoundTo = 0;

    bool timerIsRunning = false;

    public IEnumerator StartTime(float duration, bool overrideCurrentTimer = false)
    {
        if (timerIsRunning && !overrideCurrentTimer)
        {
            // Do not start timers
            Debug.LogError("Timer already running");
        }
        else if(timerIsRunning && overrideCurrentTimer)
        {
            // Start timers, but stop previous timers from running
            this.StopAllCoroutines();
            yield return StartCoroutine(StartCountdown(duration));
        }
        else
        {
            // Start timers
            yield return StartCoroutine(StartCountdown(duration));
        }
        
    }

    private IEnumerator StartCountdown(float duration)
    {
        if (progressBar != null && textTimer != null)
        {
            onStart?.Invoke();
            // Start both timers seperatley, but only wait on one to finish
            StartCoroutine(TextTimer(duration));
            yield return StartCoroutine(ProgressBarTimer(duration, true));
        }
        else if (progressBar != null)
        {
            onStart?.Invoke();
            // Progress bar
            yield return StartCoroutine(ProgressBarTimer(duration, true));
        }
        else if (textTimer != null)
        {
            onStart?.Invoke();
            // Text timer
            yield return StartCoroutine(TextTimer(duration, true));
        }
    }

    private IEnumerator ProgressBarTimer(float duration, bool invokeOnFinish = false)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            yield return new WaitForEndOfFrame();

            timeElapsed += Time.deltaTime;


            float progress = timeElapsed / duration;
            progressBar.UpdateValue(progress, false);
        }

        if (invokeOnFinish) { onFinish?.Invoke(); }
    }

    private IEnumerator TextTimer(float duration, bool invokeOnFinish = false)
    {
        float timeElapsed = 0;

        while(timeElapsed < duration)
        {
            yield return new WaitForEndOfFrame();

            double outputNum = duration - Math.Round(timeElapsed, decimalToRoundTo);
            string output = $"{textPrefix} {outputNum} {textSuffix}";

            textTimer.text = output;

            timeElapsed += Time.deltaTime;
        }

        textTimer.text = timeFinishedText;

        if(invokeOnFinish) { onFinish?.Invoke(); }
    }

    public void TestTimer(float duration)
    {
        StartCoroutine(TestTimerRoutine(duration));
    }

    private IEnumerator TestTimerRoutine(float duration)
    {
        yield return StartCoroutine(StartTime(duration));

        Debug.Log($"Timer finished");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
