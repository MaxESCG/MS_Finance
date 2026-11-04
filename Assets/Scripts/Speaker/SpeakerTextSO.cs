using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Speaker Text", order = 1)]
public class SpeakerTextSO : ScriptableObject
{

    [SerializeField] SpeakerData[] data;

    public SpeakerData[] Data => data;


}

[Serializable]
public class SpeakerData
{
    [SerializeField] string name;
    [SerializeField, TextArea(2,20)] string text; 

}
