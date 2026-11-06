using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

    ScoreManager scoreM;


    public UnityEvent ChoiceMade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide();
    }

    public void Setup(ManagerPacket managers)
    {
        scoreM = managers.ScoreManager;
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
            uiScript.Setup(option, () => 
            { 
                Debug.Log($"Button {option.Text} pressed");
                scoreM.AddProfit(option.Profit, option.ProfitStatus);
                scoreM.AddSatisfaction(option.Satisfaction, option.SatisfactionStatus);
                Hide();

                ChoiceMade?.Invoke();
                
                
            });
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
