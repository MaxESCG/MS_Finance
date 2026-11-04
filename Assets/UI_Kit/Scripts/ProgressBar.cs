using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [Header("Gameobject references")]
    [SerializeField] GameObject background;
    [SerializeField] GameObject bar;

    [Header("Settings")]
    [SerializeField] float timeToTransition = 0.1f;

    [Header("Bar values")]
    [SerializeField][Range(0, 1)] float value;


    float barWidth;

    RectTransform barRect;


    public void TestSizing(float value)
    {
        UpdateValue(value, false);
    }

    // Given a value between 0 & 1, will set the size of the bar accordingly
    public void UpdateValue(float newValue, bool smoothUpdate = true)
    {
        if(smoothUpdate)
        {
            StartCoroutine(SmoothUpdateBar(newValue, timeToTransition));
        }
        else
        {
            UpdateBar(newValue);
        }
    }

    void UpdateBar(float newValue)
    {
        value = newValue;

        float newWidth = barWidth * value;

        Vector2 barSize = barRect.sizeDelta;
        barSize.x = newWidth;

        barRect.sizeDelta = barSize;
    }

    IEnumerator SmoothUpdateBar(float targetValue, float timeToMove)
    {
        float elapsedTime = 0;

        float startValue = value;

        while (elapsedTime < timeToMove)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / timeToMove;
            float currValue = Mathf.Lerp(startValue, targetValue, t);

            UpdateBar(currValue);

            yield return new WaitForEndOfFrame();
        }
    }

    private void Start()
    {
        barWidth = background.GetComponent<RectTransform>().sizeDelta.x;
        barRect = bar.GetComponent<RectTransform>();

        UpdateBar(value);
    }
}
