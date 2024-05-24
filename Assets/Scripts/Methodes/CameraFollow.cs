using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dirk Dynamite/CameraFollow")]
[DisallowMultipleComponent]

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 posOffset;

    void LateUpdate()
    {
        transform.position = target.position + posOffset;
    }

    /*[Header("Player")]
    public GameObject player;
    private Vector3 velocity;

    [Header("Camera")]
    public float timeOffset;
    public Vector3 posOffset;
    
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, timeOffset);
    }*/
}
