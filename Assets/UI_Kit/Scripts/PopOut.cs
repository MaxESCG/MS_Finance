using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopOut : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject panel;
    [SerializeField] Button openButton;
    [SerializeField] Animation popupAnim;
    [SerializeField] AnimationClip openPopup;
    [SerializeField] AnimationClip closePopup;

    [Header("Event")]
    [SerializeField] UnityEvent<bool> onPopupToggle;

    [Header("Options")]
    [SerializeField] bool startOpen = false;




    private string openAnimString = "Open";
    private string closeAnimString = "Close";

    private bool isOpen;

    public bool IsOpen => isOpen;
    public UnityEvent<bool> OnToggleEvent => onPopupToggle;

    public void OpenPopup()
    {
        StartCoroutine(OpenPopupSequence());
    }

    public void ClosePopup()
    {
        StartCoroutine(ClosePopupSequence());
    }

    IEnumerator OpenPopupSequence()
    {
        TogglePopup(true);
        // Play anim        
        popupAnim.Play(openAnimString);
        yield return new WaitForSeconds(openPopup.length);

        
    }

    IEnumerator ClosePopupSequence()
    {
        //Play close anim
        popupAnim.Play(closeAnimString);
        yield return new WaitForSeconds(closePopup.length);

        TogglePopup(false);
    }

    void TogglePopup(bool open)
    {
        panel.gameObject.SetActive(open);
        openButton.interactable = !open;

        isOpen = open;
        onPopupToggle?.Invoke(isOpen);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openPopup.legacy = true;
        closePopup.legacy = true;

        popupAnim.AddClip(openPopup, openAnimString);
        popupAnim.AddClip(closePopup, closeAnimString);

        if (startOpen)
        {
            TogglePopup(true);
        }
        else
        {
            TogglePopup(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class PopOutToggled : UnityEvent<bool, int> { }