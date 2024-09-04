using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    public Slider slider;
    public SliderType sliderType; 

    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();

        switch (sliderType)
        {
            case SliderType.Music:
                slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
                break;
            case SliderType.Sounds:
                slider.value = PlayerPrefs.GetFloat("SFXVolume", 0.8f);
                break;
            default:
                break;
        }
    }

    // Function called when value of connected slider is changed
    public void ValueChange()
    {
        switch (sliderType)
        {
            case SliderType.Music:
                SettingsManager.instance.SetMusicVolume(slider.value);
                Debug.Log("Music value changed to: " + slider.value);
                break;
            case SliderType.Sounds:
                SettingsManager.instance.SetSFXVolume(slider.value);
                Debug.Log("Sound value changed to: " + slider.value);
                break;
            default:
                Debug.Log("Unknown value type: " + sliderType.ToString() + ". Value available: Music, Sounds");
                break;
        }
    }

    void Update()
    {
        switch (sliderType)
        {
            case SliderType.Music:
                SettingsManager.instance.masterMixer.SetFloat("MusicParam", Mathf.Lerp(-80, 0, slider.value));
                break;
            case SliderType.Sounds:
                SettingsManager.instance.masterMixer.SetFloat("SoundParam", Mathf.Lerp(-80, 0, slider.value));
                break;
            default:
                break;
        }
    }
}

public enum SliderType
{
    Music,
    Sounds
}
