using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public AudioClip uiChime;
    AudioSource audioSource;
    private float _coolDownTime;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _coolDownTime -= Time.deltaTime;
    }
    
    public void PlayPreviewSound()
    {
        if (_coolDownTime <= 0) {
            audioSource.PlayOneShot(uiChime);
            _coolDownTime = uiChime.length;
        }
    }
}
