using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/CustomRules/StatueDecayList", fileName = "Statue Decay List")]   
public class StatueDecayList : ScriptableObject
{
    public StatueDecay[] list;

    public List<string> GetOptionNames()
    {
        List<string> keys = new List<string>();
        
        foreach (StatueDecay statueDecay in list)
        {
            keys.Add(statueDecay.optionName);
        }

        return keys;
    }
}