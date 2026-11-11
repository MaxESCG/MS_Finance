using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameOver", order = 1)]

public class GameOverSO : SequenceStepSO
{
    [SerializeField] SequenceSO sequenceToRestartFrom;
    [SerializeField] string message;

    public SequenceSO SequenceToRestartFrom => sequenceToRestartFrom;
    public string Message => message;

    
}