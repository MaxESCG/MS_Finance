using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject innerContainer;

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI body;
    [SerializeField] GameObject optionsContainer;

    [Header("Prefabs")]
    [SerializeField] GameObject optionPrefab;

    List<GameObject> currentOptions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewChoice(ChoiceSO choice)
    {
        Clear();
        Show();

        title.text = choice.ChoiceText;

        for(int i = 0; i < choice.Options.Length; i++)
        {
            AddNewOption(choice.Options[i]);
        }

    }

    private void AddNewOption(Option option)
    {
        if(currentOptions == null) { currentOptions = new List<GameObject>(); }

        GameObject newGO = GameObject.Instantiate(optionPrefab, optionsContainer.transform);
        OptionUIElement uiScript = newGO.GetComponent<OptionUIElement>();

        if (uiScript != null)
        {
            uiScript.Setup(option);
        }

        currentOptions.Add(newGO);
    }

    private void Clear()
    {
        if(currentOptions == null) { return; }

        for(int i = 0; i < currentOptions.Count; i++)
        {
            GameObject.Destroy(currentOptions[i]);
        }

        currentOptions = null;
    }

    private void Hide()
    {
        innerContainer.SetActive(false);

    }

    private void Show()
    {
        innerContainer.SetActive(true);
    }
}
