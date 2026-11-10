using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpeakerManager : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] TextMeshProUGUI outputText;
    [SerializeField] Image personImage;

    SpeakerTextSO currentSpeakerData;
    int currTextID;

    public UnityEvent SpeakingFinished;

    public void SetNewConversation(SpeakerTextSO textData)
    {
        currentSpeakerData = textData;
        currTextID = 0;

        Show();

        UpdateVisuals();
    }

    public void NextBtnPress()
    {
        currTextID++;
        // check for overflow
        if(currTextID >= currentSpeakerData.Dialogues.Length)
        {
            FinishRoutine();
        }
        else
        {
            UpdateVisuals();
        } 
    }

    private void UpdateVisuals()
    {
        DialogueData currData = currentSpeakerData.Dialogues[currTextID];

        outputText.text = currData.Text;

        Sprite img = currData.Character.GetCharacterEmotion(currData.Emotion);
        personImage.sprite = img;
    }

    private void FinishRoutine()
    {
        Debug.Log("It's over wrap it up");
        Hide();
        SpeakingFinished?.Invoke();
    }

    private void Start()
    {
        Hide();
    }

    private void Hide()
    {
        container.gameObject.SetActive(false);
    }

    private void Show()
    {
        container.SetActive(true);
    }
}
