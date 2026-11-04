using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] Fade[] fades;

    public void FadeSequence()
    {
        for (int i = 0; i < fades.Length; i++)
        {
            fades[i].FadeSequence();
        }
    }

    public void FadeOut()
    {
        for (int i = 0; i < fades.Length; i++)
        {
            fades[i].FadeOut();
        }
    }

    public void FadeIn()
    {
        for (int i = 0; i < fades.Length; i++)
        {
            fades[i].FadeIn();
        }
    }
}