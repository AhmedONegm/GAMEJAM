using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = AudioManager.Instance.MusicVolume;
        sfxSlider.value = AudioManager.Instance.SFXVolume;

        musicSlider.onValueChanged.AddListener(volume => AudioManager.Instance.SetMusicVolume(volume));
        sfxSlider.onValueChanged.AddListener(volume => AudioManager.Instance.SetSFXVolume(volume));
    }

    private void OnEnable()
    {
        if (AudioManager.Instance != null)
        {
            musicSlider.value = AudioManager.Instance.MusicVolume;
            sfxSlider.value = AudioManager.Instance.SFXVolume;
        }
    }
}
