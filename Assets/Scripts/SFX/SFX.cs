using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    // public AudioClip[] audioClips;
    public static SFX Instance { get; private set; }
    public Transform soundPlayerPrefab;
    
    public AudioClip[] audioClips;
    private Dictionary<string, AudioClip> _sounds = new Dictionary<string, AudioClip>();

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioClip clip in audioClips)
        {
            _sounds.Add(clip.name, clip);
        }
    }

    // Play a sound loaded into the SFX bank
    public void PlaySound(string soundName, Vector3 position = default, bool singleton = false)
    {
        if (singleton)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name == soundName)
                {
                    return;
                }
            }
        }
        
        AudioClip sound = _sounds.GetValueOrDefault(soundName);
        Transform instance = Instantiate(soundPlayerPrefab, transform);
        instance.gameObject.name = soundName;
        
        AudioSource audioSource = instance.GetComponent<AudioSource>();
        audioSource.clip = sound;
        instance.position = position;
        
        // Debug.Log("[SFX] Playing sound " + soundName);
        StartCoroutine(PlaySoundAndWait(audioSource, instance.gameObject));
    }

    // Play an AudioClip sound
    public void PlaySound(AudioClip sound, Vector3 position = default, bool singleton = false)
    {
        if (singleton)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name == sound.name)
                {
                    return;
                }
            }
        }
        
        Transform instance = Instantiate(soundPlayerPrefab, transform);
        instance.gameObject.name = sound.name;
        
        AudioSource audioSource = instance.GetComponent<AudioSource>();
        audioSource.clip = sound;
        instance.position = position;
        
//        Debug.Log("[SFX] Playing sound " + sound.name);
        StartCoroutine(PlaySoundAndWait(audioSource, instance.gameObject));
    }

    public void PlayDelayedSound(AudioClip sound, float seconds, Vector3 position = default, bool singleton = false)
    {
        StartCoroutine(_PlayDelayedSound(sound, seconds, position, singleton));
    }

    private IEnumerator _PlayDelayedSound(AudioClip sound, float seconds, Vector3 position = default, bool singleton = false)
    {
        yield return new WaitForSeconds(seconds);
        PlaySound(sound, position, singleton);
    }

    // Play sound, destroy SoundPlayer when done
    private IEnumerator PlaySoundAndWait(AudioSource audioSource, GameObject instance)
    {
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        Destroy(instance);
    }
}
