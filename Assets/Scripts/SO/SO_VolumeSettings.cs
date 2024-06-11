using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "VolumeSettings", menuName = "Dirk Dynamite/VolumeSettings")]

public class SO_VolumeSettings : ScriptableObject
{
    
    [Range(0.0001f, 1f)]
    public float mainVolume;
    [Range(0.0001f, 1f)]
    public float musicVolume;
    [Range(0.0001f, 1f)]
    public float effectsVolume;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
