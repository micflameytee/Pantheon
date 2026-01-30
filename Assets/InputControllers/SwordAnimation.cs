using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    [SerializeField] private Transform SwingPoint;

    public float SwingTime;
    public float StartAngle;
    public float EndAngle;

    private void Awake()
    {
        SetAnimationPercent(0f);
    }

    private void SetAnimationPercent(float percent)
    {
        float sinAngle = Mathf.Sin(percent * 180f / 50f);
        
        float angle = Mathf.Lerp(StartAngle, EndAngle, sinAngle);
        SwingPoint.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
    
    private IEnumerator SwingRoutine()
    {
        float swingTimer = 0f;
        while (swingTimer < SwingTime)
        {
            float percentage = swingTimer / SwingTime;
            SetAnimationPercent(percentage);
            yield return null;
            swingTimer += Time.deltaTime;
        }
    }

    
    public void PerformAnimation()
    {
        StartCoroutine(SwingRoutine());
    }
}
