using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public string playerCollectTag;
    public string playerTag;
    public GameObject infoBox;
    public UnityEvent OnPickUp;
    public int itemValue;
    public float destroyDelay = 0.5f;
    public ItemType type;

    public enum ItemType
    {
        HEAL, STAMINA
    }

    public void Collect()
    {
        OnPickUp.Invoke();
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerCollectTag))
        {
            if(type == ItemType.HEAL)
            {
                HealthManager manager = collision.transform.parent.GetComponent<HealthManager>();
                manager.Regen(itemValue);
                Collect();
            }
            else if(type == ItemType.STAMINA)
            {
                StaminaManager manager = collision.transform.parent.GetComponent<StaminaManager>();
                manager.Regen(itemValue);
                Collect();
            }
            else
            {
                return;
            }
        }
        else if (collision.CompareTag(playerTag))
        {
            infoBox.SetActive(true);
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
