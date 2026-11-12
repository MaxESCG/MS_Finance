using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{

    private string resultMsg;

    private int satisfaction;
    private int profit;

    private Status satisfactionStatus;
    private Status profitStatus;

    private int changeInSatisfaction;
    private int changeInProfit;

    public UnityEvent<CurrentScores> ValuesUpdated;

    public int Satisfaction => satisfaction;
    public int Profit => profit;

    public void Setup(int startingSatisfaction, int startingProfit)
    {
        satisfaction = startingSatisfaction;
        profit = startingProfit;

        ValuesUpdated?.Invoke(GetScores());
    }

    public void AddSatisfaction(int satisfaction, Status newStatus)
    {
        this.satisfaction += satisfaction;
        this.satisfactionStatus = newStatus;

        changeInSatisfaction += satisfaction;

        ValuesUpdated?.Invoke(GetScores());
    }

    public void AddProfit(int profit, Status newStatus)
    {
        this.profit += profit;
        this.profitStatus = newStatus;

        changeInProfit += profit;

        ValuesUpdated?.Invoke(GetScores());
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

    private CurrentScores GetScores()
    {
        CurrentScores scores = new CurrentScores();
        scores.Satisfaction = this.satisfaction;
        scores.Profit = this.profit;

        return scores;
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

public class CurrentScores
{
    public int Satisfaction;
    public int Profit;
}

public enum Status
{
    NoChange,
    Secure,
    AtRisk
}