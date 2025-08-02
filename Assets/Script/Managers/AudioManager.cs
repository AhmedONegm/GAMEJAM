using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }
    private AudioSource musicAudioSource;
    public Sound[] soundsSFX;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            musicAudioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
    }

    [SerializeField] private AudioMixer audioMixer;

    public float MusicVolume { get; private set; } = -20f;
    public float SFXVolume { get; private set; } = -20f;

    private void Start()
    {
        musicAudioSource.Play();
        LoadPlayerPrefs();
        ApplyVolumeSettings();
        foreach (Sound sound in soundsSFX)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.loop = sound.isLooping;
        }
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
        audioMixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadPlayerPrefs()
    {
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", -20f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", -20f);
    }

    private void ApplyVolumeSettings()
    {
        audioMixer.SetFloat("Music", MusicVolume);
        audioMixer.SetFloat("SFX", SFXVolume);
    }
    public void PlaySpecificSound(string audioName)
    {
        Sound s = Array.Find(soundsSFX, sounds => sounds.audioName == audioName);
        if (s != null)
        {
            Debug.Log("Playing sound: " + audioName);
            s.audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + audioName);
        }
    }
}
