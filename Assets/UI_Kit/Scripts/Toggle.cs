using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Toggle : MonoBehaviour
{
    [Header("Gameobjects")]
    [SerializeField] GameObject background;
    [SerializeField] GameObject knob;
    [SerializeField] GameObject offPoint;
    [SerializeField] GameObject onPoint;

    [Header("Settings")]
    [Header("Visual settings")]
    [SerializeField] Color onTrueColour;
    [SerializeField] bool isTrue;
    [Header("Animation settings")]
    [SerializeField] float transitionTime = 0.25f;
    [SerializeField] int sideOffset = 2;

    [Header("Events")]
    [SerializeField] ToggleEvent onToggle;

    [Header("Toggle parameters - Changes may break things!")]
    [SerializeField] private ToggleType type;


    // Code only variables
    UnityEngine.UI.Button button;

    Color defaultColour;

    UnityEngine.UI.Image backgroundImg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get Compoenents
        backgroundImg = background.GetComponent<UnityEngine.UI.Image>();

        // Setup button for interactivity
        button = gameObject.AddComponent<UnityEngine.UI.Button>();
        button.targetGraphic = background.GetComponent<Graphic>();

        button.onClick.AddListener(OnPress);

        defaultColour = backgroundImg.color;

        // Make sure button represents current state
        if (isTrue) OnTrue(0);
        else OnFalse(0);
    }

    void OnPress()
    {
        isTrue = !isTrue;

        if (isTrue) OnTrue(transitionTime);
        else OnFalse(transitionTime);

        onToggle?.Invoke(isTrue);
    }

    void OnTrue(float time)
    {
        // Change colour
        backgroundImg.color = onTrueColour;

        // Move knob to new position
        float newXPos = onPoint.transform.position.x - 30 - sideOffset;

        StartCoroutine(MoveKnob(newXPos, time));
    }

    void OnFalse(float time)
    {
        // Change colour
        backgroundImg.color = defaultColour;

        // Move knob to new position
        float newXPos = offPoint.transform.position.x - 30 + sideOffset;

        StartCoroutine(MoveKnob(newXPos, time));

    }

    IEnumerator MoveKnob(float position, float time)
    {
        float startPosition = knob.transform.position.x;

        float timer = 0;

        
        while(timer < time)
        {

            float t = timer / time;
            float newX = Mathf.Lerp(startPosition, position, t);

            MoveKnobTo(newX);

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }

        MoveKnobTo(Mathf.Lerp(startPosition, position, 1));
    }

    void MoveKnobTo(float pos)
    {
        Vector2 newVec = knob.transform.position;
        newVec.x = pos;

        knob.transform.position = newVec;
    }
}

public enum ToggleType
{
    Checkbox,
    Bar
}

[Serializable]
public class ToggleEvent : UnityEvent <bool>{ }
