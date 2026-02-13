using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/CustomRules/StatueDecay", fileName = "Statue Decay")]   
public class StatueDecay : ScriptableObject
{
    public string optionName;
    public float lengthMinutes;

    public float GetLengthSeconds()
    {
        return lengthMinutes * 60;
    }
}