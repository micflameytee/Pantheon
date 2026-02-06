using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBar : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject glue;
    [SerializeField] private GameObject ability;

    public enum TrapType
    {
        Bomb,
        Glue
    };
    
    public void SetBomb(bool visible)
    {
        bomb.SetActive(visible);
    }
    
    public void SetGlue(bool visible)
    {
        glue.SetActive(visible);
    }
}
