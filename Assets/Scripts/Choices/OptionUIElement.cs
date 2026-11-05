using TMPro;
using UnityEngine;

public class OptionUIElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void Setup(Option option)
    {
        text.text = option.Text;
    }
}