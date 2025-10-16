using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource gunSource;         
    [SerializeField] private AudioSource backgroundMusic;    

    [Header("Audio Clips")]
    [SerializeField] private List<AudioClip> clips;       
    private Dictionary<string, AudioClip> clipDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        clipDict = new Dictionary<string, AudioClip>();
        foreach (var clip in clips)
        {
            clipDict[clip.name] = clip; 
        }
    }

    public void PlayGunSound(string clipName)
    {
        if (clipDict.TryGetValue(clipName, out AudioClip clip))
        {
            gunSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"[AudioManager] Sound '{clipName}' not found in clip list!");
        }
    }

    public void PlayMusic(string clipName, bool loop = true)
    {
        if (clipDict.TryGetValue(clipName, out AudioClip clip))
        {
            backgroundMusic.clip = clip;
            backgroundMusic.loop = loop;
            backgroundMusic.Play();
        }
        else
        {
            Debug.LogWarning($"[AudioManager] Music '{clipName}' not found in clip list!");
        }
    }

    public void StopMusic()
    {
        backgroundMusic.Stop();
    }

    public void PlaySoundAtPoint(string clipName, Vector3 pos, float volume = 1f)
    {
        if (clipDict.TryGetValue(clipName, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip, pos, volume);
        }
        else
        {
            Debug.LogWarning($"[AudioManager] Sound '{clipName}' not found in clip list!");
        }
    }
}


