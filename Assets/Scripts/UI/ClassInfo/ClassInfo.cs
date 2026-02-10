using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/UI/ClassInfo", fileName = "Class Info")]   
public class ClassInfo : ScriptableObject
{
    public string className;
    [Multiline] public string description;
    public Sprite sprite;
}
