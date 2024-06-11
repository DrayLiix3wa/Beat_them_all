using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeMixer : MonoBehaviour
{

    public AudioMixer mixer;
    public string volumeParameterName;
    
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
        mixer.SetFloat(volumeParameterName, decibel);
    }

}
