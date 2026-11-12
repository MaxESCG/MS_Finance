using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] private SpeakerManager speakerManager;
    [SerializeField] private ResultsUI results;
    [SerializeField] private ChoiceManager choiceManager;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private ScoreUI scoreUI;

    private Queue<SequenceSO> sequenceQueue;

    private SequenceSO currentSequence;
    private int currStepID;

    public void StartProcess()
    {
        StartNextSequence();
    }

    public void QueueSequence(SequenceSO sequence)
    {
        if(sequenceQueue == null) { sequenceQueue = new Queue<SequenceSO>(); }

        Debug.Log($"Queing Sequence: {sequence.name}");
        sequenceQueue.Enqueue(sequence);
    }

    public void OnSequenceStepFinished()
    {
        speakerManager.SpeakingFinished.RemoveAllListeners();
        choiceManager.ChoiceMade.RemoveAllListeners();
        results.FinishedViewingResults.RemoveAllListeners();
        gameOverUI.FinishedGameOver.RemoveAllListeners();

        // If there are more steps in the sequence, continue sequence, otherwise, start next sequence.

        Debug.Log($"--- Sequence step finished---");

        if(currStepID < currentSequence.Steps.Length - 1)
        {
            Debug.Log($"Strating next step");
            StartNextStep();
        }
        else
        {
            Debug.Log($"Next Sequence");
            StartNextSequence();
        }
    }

    private void StartNextStep()
    {
        currStepID++;

        if(currentSequence.Steps.Length <= 0) { Debug.LogError($"--- No steps set to current sequence {currentSequence.name} ---"); }

        SequenceStepSO currStep = currentSequence.Steps[currStepID];

        Debug.Log($"Curr step type: {currStep.GetType()}");

        if(currStep.showScore)
        {
            scoreUI.Show();
        }
        else
        {
            scoreUI.Hide();
        }

        if (currStep.GetType() == typeof(SpeakerTextSO))
        {
            // Start dialogue 
            speakerManager.SetNewConversation((SpeakerTextSO)currStep);
            speakerManager.SpeakingFinished.AddListener(() =>
            {
                OnSequenceStepFinished();
            });
        }
        else if (currStep.GetType() == typeof(ChoiceSO))
        {
            // Start choice
            choiceManager.StartNewChoice((ChoiceSO)currStep);
            choiceManager.ChoiceMade.AddListener(() =>
            {
                OnSequenceStepFinished();
            });

        }
        else if (currStep.GetType() == typeof(ResultsSO))
        {
            // Start result
            results.SetResults();
            results.FinishedViewingResults.AddListener(() =>
            {
                OnSequenceStepFinished();
            });
        }
        else if (currStep.GetType() == typeof(SequenceQueuerStep))
        {
            Debug.Log($"Queuer called");
            SequenceQueuerStep step = (SequenceQueuerStep)currStep;

            QueueSequence(step.NextSequence);
            OnSequenceStepFinished();
        }
        else if (currStep.GetType() == typeof(GameOverSO))
        {
            GameOverSO gameOverSO = (GameOverSO)currStep;

            gameOverUI.CallGameOver(gameOverSO);

            gameOverUI.FinishedGameOver.AddListener(() =>
            {
                OnSequenceStepFinished();
            });
        }
        else if (currStep.GetType() == typeof(ScoreModifierSO))
        {
            ScoreModifierSO newScores = (ScoreModifierSO)currStep;

            if (newScores.ModifyType == ScoreModifyType.Set)
            {
                scoreManager.Setup(newScores.Satisfaction, newScores.Profit);
            }
            else if (newScores.ModifyType == ScoreModifyType.Add)
            {
                scoreManager.AddSatisfaction(newScores.Satisfaction, newScores.SatisfactionStatus);
                scoreManager.AddProfit(newScores.Profit, newScores.ProfitStatus);
            }

            OnSequenceStepFinished();
        }
        else if (currStep.GetType() == typeof(PreviousChoiceSplit))
        {
            PreviousChoiceSplit previousChoiceSplit = (PreviousChoiceSplit)currStep;

            string resultID = choiceManager.GetOptionChosenID(previousChoiceSplit.ChoiceToEvaluate.Id);

            for (int i = 0; i < previousChoiceSplit.ChoiceResults.Length; i++)
            {
                if (previousChoiceSplit.ChoiceResults[i].OptionID == resultID)
                {
                    QueueSequence(previousChoiceSplit.ChoiceResults[i].ResultingSequence);
                }
            }

            OnSequenceStepFinished();
        }
        else if (currStep.GetType() == typeof(SequenceScoreSplitSO))
        {
            SequenceScoreSplitSO scoreSplitSO = (SequenceScoreSplitSO)currStep;

            SequenceSO sequenceToQueue = null;

            int baseNum = 0;
            if (scoreSplitSO.ScoreType == ScoreTypeToCheck.Satisfaction)
            {
                Debug.Log($"BaseNum type: {scoreSplitSO.ScoreType} = {scoreManager.Satisfaction}");
                baseNum = scoreManager.Satisfaction;
            }
            else if (scoreSplitSO.ScoreType == ScoreTypeToCheck.Profit)
            {
                Debug.Log($"BaseNum type: {scoreSplitSO.ScoreType} = {scoreManager.Profit}");
                baseNum = scoreManager.Profit;
            }

            for (int i = 0; i < scoreSplitSO.ScoreSplits.Length; i++)
            {
                ScoreSplitRegion currRegion = scoreSplitSO.ScoreSplits[i];

                int upperNum = currRegion.UpperLimit;
                int lowerNum = currRegion.LowerLimit;

                Debug.Log($"Upper: {upperNum}: lower {lowerNum}");
                Debug.Log($"in region of? {baseNum >= lowerNum && baseNum < upperNum}");

                if(baseNum >= lowerNum && baseNum < upperNum)
                {
                    Debug.Log($"Queing: {sequenceToQueue}");
                    sequenceToQueue = currRegion.ResultingSequence;
                }
            }

            Debug.Log($"Sequence to queue: {sequenceToQueue}");
            if(sequenceToQueue != null)
            {
                
                sequenceQueue.Enqueue(sequenceToQueue);
            }

            OnSequenceStepFinished();
        }
    }

    private void StartNextSequence()
    {
        if(sequenceQueue.Count <= 0)
        {
            Debug.LogError("No sequence queued");
            return;
        }

        currentSequence = sequenceQueue.Dequeue();

        if (currentSequence == null) { Debug.LogError("No new step found"); }

        currStepID = -1;
        StartNextStep();
    }
}