using Unity.VisualScripting;
using UnityEngine;

public class ManagerPacket
{
    public SpeakerManager SpeakerManager;
    public ChoiceManager ChoiceManager;
    public ResultsUI ResultsUI;
    public ScoreManager ScoreManager;


    public ManagerPacket(SpeakerManager speakerM, ChoiceManager choiceM, ResultsUI resultsUI, ScoreManager scoreM)
    {
        this.SpeakerManager = speakerM;
        this.ChoiceManager = choiceM;
        this.ResultsUI = resultsUI;
        this.ScoreManager = scoreM;
    }
}
