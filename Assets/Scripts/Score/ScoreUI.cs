using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] GameObject innerContainer;
    [SerializeField] TextMeshProUGUI customerSatisfactionText;
    [SerializeField] TextMeshProUGUI profitText;

    [SerializeField] ScoreManager scoreManager;

    private void Start()
    {
        scoreManager.ValuesUpdated.AddListener(UpdateScores);
        Hide();
    }

    private void UpdateScores(CurrentScores currentScores)
    {
        customerSatisfactionText.text = $"{currentScores.Satisfaction}%";
        profitText.text = $"{currentScores.Profit}%";
    }

    public void Hide()
    {
        innerContainer.SetActive(false);
    }

    public void Show()
    {
        innerContainer.SetActive(true);
    }
}