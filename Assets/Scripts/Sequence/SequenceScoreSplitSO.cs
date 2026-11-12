using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScoreSplit", order = 1)]
public class SequenceScoreSplitSO : SequenceStepSO
{
    [SerializeField] private ScoreTypeToCheck scoreType;

    [SerializeField] private ScoreSplitRegion[] scoreSplits;

    public ScoreSplitRegion[] ScoreSplits => scoreSplits;
    public ScoreTypeToCheck ScoreType => scoreType;
}

[Serializable]
public class ScoreSplitRegion
{
    [SerializeField] private int lowerLimit;
    [SerializeField] private int upperLimit;
    [SerializeField] SequenceSO resultingSequence;

    
    public int LowerLimit => lowerLimit;
    public int UpperLimit => upperLimit;
    public SequenceSO ResultingSequence => resultingSequence;
}

public enum ScoreTypeToCheck
{ 
    Satisfaction,
    Profit
}