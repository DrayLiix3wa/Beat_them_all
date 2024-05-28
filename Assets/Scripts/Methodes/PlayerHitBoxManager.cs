using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitBoxManager : MonoBehaviour
{

    public string[] damageTags;
    public PlayerController playerController;
    public AudioController audioController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Got Hit !");
        
        if (damageTags.Contains(collision.tag))
        {
            if (playerController._blockActive)
            {
                if (playerController.staminaManager.current >= playerController.blockCost)
                {
                    playerController.staminaManager.Consume(playerController.blockCost);
                    audioController.PlayBlockSound();
                }
                else
                {
                    playerController.damageTaken = collision.GetComponent<HitDamageValue>().hitDamageValue;
                    playerController._isHurting = true;
                }
            }
            else
            {
                playerController.damageTaken = collision.GetComponent<HitDamageValue>().hitDamageValue;
                playerController._isHurting = true;
            }
        }
    }
}
