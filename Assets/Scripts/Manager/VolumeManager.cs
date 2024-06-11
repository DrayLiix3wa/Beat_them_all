using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static VolumeMixer;

public class VolumeManager : MonoBehaviour
{

    public AudioMixer mixer;
    public SO_VolumeSettings volumeSettings;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        float decibel1 = Mathf.Log10(volumeSettings.mainVolume) * 20f;
        mixer.SetFloat("MasterVolume", decibel1);

        float decibel2 = Mathf.Log10(volumeSettings.musicVolume) * 20f;
        mixer.SetFloat("MusicVolume", decibel2);

        float decibel3 = Mathf.Log10(volumeSettings.effectsVolume) * 20f;
        mixer.SetFloat("SFXVolume", decibel3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume(float value)
    {

    }
}
