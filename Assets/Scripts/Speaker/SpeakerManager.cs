using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI outputText;
    [SerializeField] Image personImage;

    public void SetNewConversation()
    {

    }

    public void NextBtnPress()
    {
        outputText.text = "yoyo";
    }
}
