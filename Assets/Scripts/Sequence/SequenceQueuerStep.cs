using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SequenceQueuer", order = 1)]
public class SequenceQueuerStep : SequenceStepSO
{
    [SerializeField] SequenceSO nextSequence;

    public SequenceSO NextSequence => nextSequence;
}
