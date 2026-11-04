using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ConfirmationBox : MonoBehaviour
{
    [Header("Strings")]
    [SerializeField] string titlePrefix;
    [SerializeField] string titleMain;
    [SerializeField] string titleSuffix;
    [SerializeField] string confirmString;
    [SerializeField] string denyString;
    [SerializeField] bool showFadeOut = true;

    [Header("Objects")]
    [SerializeField] GameObject confirmationBox;
    [SerializeField] GameObject panel;
    [SerializeField] Fade fade;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI confirmText;
    [SerializeField] TextMeshProUGUI denyText;

    [Header("Events")]
    [SerializeField] UnityEvent<bool> onUserChoice;
    [SerializeField] UnityEvent onShowConfirm;


    bool screenShowing = false;

    private void ToggleConfirm(bool show)
    {
        confirmationBox.SetActive(show);
        screenShowing = show;
    }

    private void TogglePanel(bool show)
    {
        panel.SetActive(show);
    }

    public void ShowConfirmation()
    {
        StartCoroutine(ShowConfirmationSequence());
    }


    public void OnConfirm()
    {
        StartCoroutine(OnUserChoice(true, showFadeOut));
        
    }

    public void OnDeny()
    {
        StartCoroutine(OnUserChoice(false, showFadeOut));
    }

    private IEnumerator OnUserChoice(bool choice, bool showFadeOut)
    {
        onUserChoice?.Invoke(choice);

        TogglePanel(false);

        if (showFadeOut)
        {
            yield return StartCoroutine(fade.FadeInRoutine(fade.TimeToFade));
        }

        
        ToggleConfirm(false);
    }

    IEnumerator ShowConfirmationSequence()
    {
        TogglePanel(false);
        ToggleConfirm(true);
        yield return StartCoroutine(fade.FadeOutRoutine(fade.TimeToFade));

        TogglePanel(true);
        
        onShowConfirm?.Invoke();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        title.text = $"{titlePrefix} {titleMain} {titleSuffix}";
        confirmText.text = $"{confirmString}";
        denyText.text = $"{denyString}";

        ToggleConfirm(false);
    }

    public void TestOnUserChoice(bool confirmed)
    {
        Debug.Log($"Confirmed: {confirmed}");
    }

    public void TestShowConfirm()
    {
        Debug.Log($"Show confirmation box");
    }
    
}
