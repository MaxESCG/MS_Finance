using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterSO : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] CharacterEmotions neutral;
    [SerializeField] CharacterEmotions happy;
    [SerializeField] CharacterEmotions angry;
}

[Serializable]
public class CharacterEmotions
{
    [SerializeField] Emotions emotion;
    [SerializeField] Sprite image;
}

public enum Emotions{
    Neutral,
    Happy,
    Angry
}