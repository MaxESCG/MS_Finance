using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOverUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject innerContainer;
    [SerializeField] TextMeshProUGUI body;

    [Header("Managers")]
    [SerializeField] SequenceManager sequenceManager;

    private GameOverSO currGameOverData;

    public UnityEvent FinishedGameOver;

    public void CallGameOver(GameOverSO gameOverData)
    {
        currGameOverData = gameOverData; 

        body.text = currGameOverData.Message;

        Show();
    }

    public void OnPlayAgainPressed()
    {
        sequenceManager.QueueSequence(currGameOverData.SequenceToRestartFrom);

        currGameOverData = null;

        Hide();
        FinishedGameOver?.Invoke();
    }

    private void Start()
    {
        Hide();
    }


    private void Show()
    {
        innerContainer.SetActive(true);
    }

    private void Hide()
    {
        innerContainer.gameObject.SetActive(false);
    }
}

public class GameOverData
{
    public string gameOverMessage;
    public SequenceSO checkpointSequence;
}