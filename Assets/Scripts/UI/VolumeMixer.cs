using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class VolumeMixer : MonoBehaviour
{

    public AudioMixer mixer;
    public SO_VolumeSettings volumeSettings;
    public enum SliderState
    {
        MAIN, MUSIC, SFX,
    }

    public SliderState currentState;

    void Awake()
    {
        switch (currentState)
        {
            case SliderState.MAIN:
                GetComponent<Slider>().value = volumeSettings.mainVolume;
                float decibel1 = Mathf.Log10(volumeSettings.mainVolume) * 20f;
                mixer.SetFloat("MasterVolume", decibel1);
                break;
            case SliderState.MUSIC:
                GetComponent<Slider>().value = volumeSettings.musicVolume;
                float decibel2 = Mathf.Log10(volumeSettings.musicVolume) * 20f;
                mixer.SetFloat("MusicVolume", decibel2);
                break;
            case SliderState.SFX:
                GetComponent<Slider>().value = volumeSettings.effectsVolume;
                float decibel3 = Mathf.Log10(volumeSettings.effectsVolume) * 20f;
                mixer.SetFloat("SFXVolume", decibel3);
                break;
            default:
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume( float value)
    {
        float decibel = Mathf.Log10(value) * 20f;

        switch (currentState)
        {
            case SliderState.MAIN:
                volumeSettings.mainVolume = value;
                mixer.SetFloat("MasterVolume", decibel);
                break;
            case SliderState.MUSIC:
                volumeSettings.musicVolume = value;
                mixer.SetFloat("MusicVolume", decibel);
                break;
            case SliderState.SFX:
                volumeSettings.effectsVolume = value;
                mixer.SetFloat("SFXVolume", decibel);
                break;
            default:
                break;
        }
    }

}
