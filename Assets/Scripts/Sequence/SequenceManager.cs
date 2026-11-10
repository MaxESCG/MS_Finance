using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] private SpeakerManager speakerManager;
    [SerializeField] private ResultsUI results;
    [SerializeField] private ChoiceManager choiceManager;

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

        Debug.Log($"Sequence: {sequenceQueue}");
        sequenceQueue.Enqueue(sequence);
    }

    public void OnSequenceStepFinished()
    {
        speakerManager.SpeakingFinished.RemoveAllListeners();
        choiceManager.ChoiceMade.RemoveAllListeners();
        results.FinishedViewingResults.RemoveAllListeners();

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

        SequenceStepSO currStep = currentSequence.Steps[currStepID];

        Debug.Log($"Curr step type: {currStep.GetType()}");

        if(currStep.GetType() == typeof(SpeakerTextSO))
        {
            // Start dialogue 
            speakerManager.SetNewConversation((SpeakerTextSO)currStep);
            speakerManager.SpeakingFinished.AddListener(() => 
            { 
                OnSequenceStepFinished(); 
            });
        }
        else if(currStep.GetType() == typeof(ChoiceSO))
        {
            // Start choice
            choiceManager.StartNewChoice((ChoiceSO)currStep);
            choiceManager.ChoiceMade.AddListener(() =>
            {
                OnSequenceStepFinished(); 
            });

        }
        else if(currStep.GetType() == typeof(ResultsSO))
        {
            // Start result
            results.SetResults();
            results.FinishedViewingResults.AddListener(() =>
            {
                OnSequenceStepFinished();
            });
        }
        else if(currStep.GetType() == typeof(SequenceQueuerStep))
        {
            Debug.Log($"Queuer called");
            SequenceQueuerStep step = (SequenceQueuerStep)currStep;

            QueueSequence(step.NextSequence);
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