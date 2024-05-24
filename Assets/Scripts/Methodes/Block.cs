using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dirk Dynamite/Block")]
[DisallowMultipleComponent]

public class Block : MonoBehaviour
{
    public bool isBlocking;

    public float currentSpeed;
    public float walkSpeed;
    public float blockSpeed;

    public void OnBlockEnter()
    {
        isBlocking = true;
        currentSpeed = blockSpeed;
    }

    public void OnBlockExit()
    {
        isBlocking = false;
        currentSpeed = walkSpeed;
    }
}
