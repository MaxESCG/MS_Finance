using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Sequence", order = 1)]
public class SequenceSO : ScriptableObject
{
    [SerializeField] private SequenceStepSO[] steps;

    public SequenceStepSO[] Steps => steps;
    
}