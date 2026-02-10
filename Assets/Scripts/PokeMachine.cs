using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeMachine : MonoBehaviour
{
    
    private float fadeDuration = 5f;
    private float alpha = 255f;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> SpriteStates;
    [SerializeField] private Sprite _bigWin;
    
    
    [SerializeField] private List<Sprite> EffectSprites;

    
    public void ChangeSprite()
    {
        int randomIndex = Random.Range(0, SpriteStates.Count);
        

        _spriteRenderer.sprite = SpriteStates[randomIndex];
    }

    public void BigWin()
    {
        _spriteRenderer.sprite = _bigWin;
        fadeDuration *= 3f;
    }

    public void EffectSpriteChange(int index)
    {
        _spriteRenderer.sprite = EffectSprites[index - 1];        
    }
    
    


    void Update()
    {
        alpha -= (255f / fadeDuration) * Time.deltaTime;
        byte alphaByte = (byte)Mathf.Clamp(alpha, 0, 255);
        _spriteRenderer.color = new Color32(255, 255, 255, (byte)alphaByte);

        if (alpha <= 0f)
        {
            Destroy(gameObject);
        }
    }
    
}
