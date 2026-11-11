using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScoreSet", order = 1)]
public class ScoreSetterStepSO : SequenceStepSO
{
    [SerializeField] int startingSatisfaction;
    [SerializeField] int startingProfit;

    public int StartingSatisfaciton => startingSatisfaction;
    public int StartingProfit => startingProfit;
}
