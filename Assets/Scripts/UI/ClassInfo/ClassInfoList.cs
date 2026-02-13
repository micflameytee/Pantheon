using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/UI/ClassInfoList", fileName = "Class Info List")]   
public class ClassInfoList : ScriptableObject
{
    public List<ClassInfo> classInfoList;
}