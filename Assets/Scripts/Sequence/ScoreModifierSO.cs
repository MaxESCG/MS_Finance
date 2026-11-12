using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScoreSet", order = 1)]
public class ScoreModifierSO : SequenceStepSO
{
    [SerializeField] ScoreModifyType modifyType;
    [SerializeField] int satisfaction;
    [SerializeField] int profit;

    [SerializeField] Status satisfactionStatus;
    [SerializeField] Status profitStatus;

    public ScoreModifyType ModifyType => modifyType;
    public int Satisfaction => satisfaction;
    public int Profit => profit;
    public Status SatisfactionStatus => satisfactionStatus;
    public Status ProfitStatus => profitStatus; 
}

public enum ScoreModifyType
{
    Set,
    Add
}