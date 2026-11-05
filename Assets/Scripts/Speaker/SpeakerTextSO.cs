using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Speaker Text", order = 1)]
public class SpeakerTextSO : ScriptableObject
{
    [SerializeField] DialogueData[] dialogues;

    public DialogueData[] Dialogues => dialogues;


}

[Serializable]
public class DialogueData
{
    [SerializeField] CharacterSO character;
    [SerializeField] Emotions emotion;
    [SerializeField, TextArea(2,20)] string text;


    public CharacterSO Character => character;
    public Emotions Emotion => emotion;
    public string Text => text;
}
