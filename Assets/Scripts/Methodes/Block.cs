using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dirk Dynamite/Block")]
[DisallowMultipleComponent]

public class Block : MonoBehaviour
{
    //public bool isBlocking;

    public PlayerController playerManager;
    public Move moveManager;
    //public float currentSpeed;
    public float walkSpeed = 5f;
    public float blockSpeed = 2f;

    public void OnBlockEnter()
    {
        playerManager._blockActive = true;
        moveManager.speed = blockSpeed;
    }

    public void OnBlockExit()
    {
        playerManager._blockActive = false;
        moveManager.speed = walkSpeed;
    }
}
