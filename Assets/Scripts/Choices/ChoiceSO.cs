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
    [SerializeField, TextArea(3,20)] string text;
    [Header("Score changes")]
    [SerializeField] private int profit;
    [SerializeField] private Status profitStatus;
    [SerializeField] private int satisfaction;
    [SerializeField] private Status satisfactionStatus;


    public string Text => text;

    public int Profit => profit;
    public Status ProfitStatus => profitStatus;
    public int Satisfaction => satisfaction;
    public Status SatisfactionStatus => satisfactionStatus;
}