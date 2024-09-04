using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance { get; private set; }
    public AudioMixer masterMixer;


    [Header("Settings")]
    public float musicVolume = 0.8f;
    public float sfxVolume = 0.8f;

    // Start is called before the first frame update. Set the instance variable into this
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadMixer();
        LoadSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
        Debug.Log("Music volume set to: " + musicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
        Debug.Log("SFX volume set to: " + sfxVolume);
    }

    public void LoadSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.8f);
        Debug.Log("Settings loaded. Music volume: " + musicVolume + " SFX volume: " + sfxVolume);
    }

    void LoadMixer()
    {
        masterMixer = Resources.Load<AudioMixer>("Sounds/MasterMixer");
    }
}
