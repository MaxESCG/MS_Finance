using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ResultsUI : MonoBehaviour
{
    [SerializeField] GameObject innerContainer;
    [SerializeField] TextMeshProUGUI body;
    [SerializeField] TextMeshProUGUI profitTxt;
    [SerializeField] TextMeshProUGUI satisfactionTxt;

    [Header("Settings")]
    [SerializeField] string profitString;
    [SerializeField] string satisfactionString;
    [SerializeField] Color goodColour;
    [SerializeField] Color badColour;

    ScoreManager scoreM;

    public UnityEvent FinishedViewingResults;

    private void Start()
    {
        Hide();
    }

    public void Setup(ScoreManager scoreManager)
    {
        scoreM = scoreManager;
    }

    
    public void SetResults()
    {
        ScoreUpdate update = scoreM.GetScoreUpdate();

        body.text = update.ResultMsg;

        Color profitColour = Color.white;
        Color satisfactionColour = Color.white;

        string profitChangeTxt = "";
        string satisfactionChangeTxt = "";

        if (update.ChangeInProfit > 0) { profitColour = goodColour; profitChangeTxt = "+"; }
        else if (update.ChangeInProfit < 0) { profitColour = badColour; profitChangeTxt = ""; }

        if (update.ChangeInSatisfaction > 0) { satisfactionColour = goodColour; satisfactionChangeTxt = "+"; }
        else if(update.ChangeInSatisfaction < 0) { satisfactionColour = badColour; satisfactionChangeTxt = ""; }

        string profitColourHex = ColorUtility.ToHtmlStringRGBA(profitColour);
        string satisfactionColourHex = ColorUtility.ToHtmlStringRGBA(satisfactionColour);


        profitTxt.text = $"{profitString} <color=#{profitColourHex}>{profitChangeTxt}{update.ChangeInProfit}</color> (now {update.NewProfit})";
        satisfactionTxt.text = $"{satisfactionString} <color=#{satisfactionColourHex}>{satisfactionChangeTxt}{update.ChangeInSatisfaction}</color> (now {update.NewSatisfaction})";

        Show();
    }

    public void OnCloseButtonPressed()
    {
        FinishedViewingResults?.Invoke();
        Hide();
    }

    public void Hide()
    {
        innerContainer.gameObject.SetActive(false);
    }

    public void Show()
    {
        innerContainer.gameObject.SetActive(true);
    }
}
