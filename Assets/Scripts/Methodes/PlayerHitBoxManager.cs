using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitBoxManager : MonoBehaviour
{

    public string[] damageTags;
    public PlayerController playerController;
    public AudioController audioController;
    public UnityEvent onGuardBreak;
    public Rigidbody2D rb2d;
    public float hurtSpeed = 1;
    public float blockSpeed = 0.5f;



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

            playerController._hurtDirection = transform.position - collision.transform.position;
            //playerController._hurtDirection = collision.transform.position - transform.position;

            if (playerController._blockActive)
            {
                if (playerController.staminaManager.current >= playerController.blockCost)
                {
                    playerController.staminaManager.Consume(playerController.blockCost);
                    playerController._impulseSpeed = blockSpeed;
                    playerController._moveBuffer = false;
                    playerController._hurtBuffer = true;
                    audioController.PlayBlockSound();
                }
                else
                {
                    onGuardBreak.Invoke();
                    playerController.damageTaken = collision.GetComponent<HitDamageValue>().hitDamageValue;
                    playerController._impulseSpeed = hurtSpeed;
                    playerController._isHurting = true;
                }
            }
            else
            {
                playerController.damageTaken = collision.GetComponent<HitDamageValue>().hitDamageValue;
                playerController._impulseSpeed = hurtSpeed;
                playerController._isHurting = true;
            }
        }
    }
}
