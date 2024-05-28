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
    private PoolsManager poolsManager;
    public SO_objectPool objectPool;

    public enum ItemType
    {
        HEAL, STAMINA
    }

    private void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag("PoolsManager");
        poolsManager = targetObject.GetComponent<PoolsManager>();
    }

    public void Collect()
    {
        OnPickUp.Invoke();
        poolsManager.ReturnObjectToPool(gameObject, objectPool.poolName);
        //Destroy(gameObject, destroyDelay);
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
