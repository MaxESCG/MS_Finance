using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionUIElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Button btn;

    public void Setup(Option option, UnityAction action)
    {
        text.text = option.Text;

        btn.onClick.AddListener(action);
    }
}