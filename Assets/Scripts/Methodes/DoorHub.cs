using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHub : MonoBehaviour
{
    public SO_Door door;
    public SpriteRenderer lightCheck;

    public string playerTag;
    public GameObject infoBox;

    private void Update()
    {
        if(door.isOpen)
        {
            lightCheck.color = Color.green;
        }
        else
        {
            lightCheck.color= Color.red;
        }
    }

    private void OnValidate()
    {
        if(door != null)
        {
            gameObject.name = "door_" + door.level.name;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            infoBox.SetActive(true);

            if (door.isOpen)
            {
                //changement de scene
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            infoBox.SetActive(false);
        }
    }
}
