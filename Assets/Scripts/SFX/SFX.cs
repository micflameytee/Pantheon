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

        // _sounds.GetValueOrDefault("arrow");
    }

    public void PlaySound(string soundName)
    {
        AudioClip sound = _sounds.GetValueOrDefault(soundName);
        Transform instance = Instantiate(soundPlayerPrefab, transform);
        
        AudioSource audioSource = instance.GetComponent<AudioSource>();
        audioSource.clip = sound;
        
        Debug.Log("[SFX] Playing sound " + soundName);
        StartCoroutine(PlaySoundAndWait(audioSource, instance.gameObject));
    }

    // Play a positional sound effect.
    public void PlaySound(string soundName, Vector3 position)
    {
        AudioClip sound = _sounds.GetValueOrDefault(soundName);
        Transform instance = Instantiate(soundPlayerPrefab, transform);
        
        AudioSource audioSource = instance.GetComponent<AudioSource>();
        audioSource.clip = sound;
        instance.position = position;
        
        Debug.Log("[SFX] Playing sound " + soundName);
        StartCoroutine(PlaySoundAndWait(audioSource, instance.gameObject));
    }

    // Play sound, destroy SoundPlayer when done
    private IEnumerator PlaySoundAndWait(AudioSource audioSource, GameObject instance)
    {
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        Destroy(instance);
    }
}
