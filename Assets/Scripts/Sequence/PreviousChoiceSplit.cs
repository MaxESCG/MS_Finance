using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PreviousChoiceSplit", order = 1)]
public class PreviousChoiceSplit : SequenceStepSO
{
    [SerializeField] private ChoiceSO choiceToEvaluate;
    [SerializeField] private PreviousChoiceResults[] choiceResults;

    public ChoiceSO ChoiceToEvaluate => choiceToEvaluate;
    public PreviousChoiceResults[] ChoiceResults => choiceResults;
}

[Serializable]
public class PreviousChoiceResults
{
    [SerializeField] private string optionID;
    [SerializeField] private SequenceSO resultingSequence;

    public string OptionID => optionID;
    public SequenceSO ResultingSequence => resultingSequence;
}