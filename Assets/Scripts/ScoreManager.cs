using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private string resultMsg;

    private int satisfaction;
    private int profit;

    private Status satisfactionStatus;
    private Status profitStatus;

    private int changeInSatisfaction;
    private int changeInProfit;

    public void Setup(int startingSatisfaction, int startingProfit)
    {
        satisfaction = startingSatisfaction;
        profit = startingProfit;
    }

    public void AddSatisfaction(int satisfaction, Status newStatus)
    {
        this.satisfaction += satisfaction;
        this.satisfactionStatus = newStatus;

        changeInSatisfaction += satisfaction;
    }

    public void AddProfit(int profit, Status newStatus)
    {
        this.profit += profit;
        this.profitStatus = newStatus;

        changeInProfit += profit;
    }

    public void AddResultMessage(string message)
    {
        resultMsg = message;
    }

    public ScoreUpdate GetScoreUpdate()
    {
        ScoreUpdate scoreUpdate = new ScoreUpdate();
        scoreUpdate.ResultMsg = this.resultMsg;

        scoreUpdate.NewProfit = profit;
        scoreUpdate.NewSatisfaction = satisfaction;

        if (changeInProfit != 0)
        {
            scoreUpdate.ChangeInProfit = changeInProfit;
        }

        if(changeInSatisfaction != 0)
        {
            scoreUpdate.ChangeInSatisfaction = changeInSatisfaction;
        }

        scoreUpdate.SatisfactionStatus = satisfactionStatus;
        scoreUpdate.ProfitStatus = profitStatus;

        changeInSatisfaction = 0;
        changeInProfit = 0;


        return scoreUpdate;
    }
}

public class ScoreUpdate
{
    public string ResultMsg;

    public int NewSatisfaction;
    public int NewProfit;

    public int ChangeInProfit;
    public int ChangeInSatisfaction;

    public Status ProfitStatus;
    public Status SatisfactionStatus;
}

public enum Status
{
    NoChange,
    Secure,
    AtRisk
}