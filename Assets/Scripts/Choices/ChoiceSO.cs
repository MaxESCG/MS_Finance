using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Choice", order = 1)]

public class ChoiceSO : ScriptableObject
{
    [SerializeField] private string choiceText;
    [SerializeField] private Option[] options;

    public string ChoiceText => choiceText;
    public Option[] Options => options;
}


[Serializable]
public class Option
{
    [SerializeField] string text;

    public string Text => text;
}