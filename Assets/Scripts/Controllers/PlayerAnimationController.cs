using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    public PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndHit()
    {
        playerController._attackAnimation = false;
    }

    public void EndBigHit()
    {
        playerController._bigAttackAnimation = false;
    }

    public void EndHurt()
    {
        playerController._hurtAnimation = false;
    }

    public void EndDeath()
    {
        playerController._deathAnimation = false;
    }
}
