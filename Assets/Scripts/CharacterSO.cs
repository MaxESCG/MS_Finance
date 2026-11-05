using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterSO : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] CharacterEmotions[] characterEmotions;

    public string Name => name;
    public CharacterEmotions[] CharacterEmotions => characterEmotions;

    public Sprite GetCharacterEmotion(Emotions emotion)
    {
        Sprite toReturn = null;
        for(int i = 0; i < characterEmotions.Length; i++)
        {
            if (characterEmotions[i].Emotion == emotion)
            {
                toReturn = characterEmotions[i].Image;
            }
        }
        return toReturn;
    }
}

[Serializable]
public class CharacterEmotions
{
    [SerializeField] Emotions emotion;
    [SerializeField] Sprite image;

    public Emotions Emotion => emotion;
    public Sprite Image => image;
}

public enum Emotions{
    Neutral,
    Happy,
    Angry
}