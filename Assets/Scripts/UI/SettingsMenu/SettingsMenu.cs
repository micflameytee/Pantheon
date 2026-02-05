using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("MasterVolume", 100));
    }

    public void SetVolume(float volume)
    {
        if (volume < 1)
        {
            // Prevent weird volume jump issue
            volume = 0.001f;
        }
        
        UpdateSlider(volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        // SFX.Instance.PlaySound("ui_chime", new Vector3(), true);
        SetVolume(volumeSlider.value);
    }

    public void UpdateSlider(float volume)
    {
        volumeSlider.value = volume;
    }

    public void LoadMainMenu()
    {
        SFX.Instance.PlaySound("ui_select");
        Utility.LoadLevel("MainMenu");
    }
}
