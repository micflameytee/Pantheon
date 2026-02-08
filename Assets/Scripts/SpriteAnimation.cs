using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprites;

    [SerializeField] private float AnimationTime;
    [SerializeField] private bool AutoPlay;
    [SerializeField] private bool LoopAnimation;
    [SerializeField] private bool DeleteOnComplete = true;

    private float _timer;
    private bool _isAnimating = true;

    public event Action OnAnimationEnd;
    
    private void Awake()
    {
        Debug.Assert(_sprites.Length > 0,  "SpriteAnimation sprite array is empty", this);
        _spriteRenderer.enabled = false; 
        if (AutoPlay)
        {
            PlayAnimation();
        }
    }

    public void SetAnimationTime(float time)
    {
        AnimationTime = time;
    }

    private void Reset()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayAnimation()
    {
        _spriteRenderer.enabled = true;
        _timer = 0;
        _isAnimating = true;
    }

    public void StopAnimation()
    {
        if (DeleteOnComplete)
        {
            _spriteRenderer.enabled = false;
        }
        OnAnimationEnd?.Invoke();
        _timer = 0;
        _isAnimating = false;
    }
    
    private void Update()
    {
        if (_isAnimating)
        {
            _timer += Time.deltaTime;
            float animationPercent = _timer / AnimationTime;
            int animationFrame = (int)(animationPercent * _sprites.Length) % _sprites.Length;
            if(_sprites[animationFrame] != null)
                _spriteRenderer.sprite = _sprites[animationFrame];

            if (!LoopAnimation && animationPercent >= 1)
            {
                StopAnimation();
            }
        }
    }
}
