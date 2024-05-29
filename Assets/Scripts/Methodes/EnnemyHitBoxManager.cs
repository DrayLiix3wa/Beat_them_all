using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnnemyHitBoxManager : MonoBehaviour
{
    public string[] damageTags;
    public EnemyController enemyController;
    public float hurtSpeed = 1;

    public UnityEvent onDamaged = new UnityEvent();

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
        Debug.Log("Ennemy Got Hit !");

        if (damageTags.Contains(collision.tag))
        {
            onDamaged.Invoke();
            enemyController.damageTaken = collision.GetComponent<HitDamageValue>().hitDamageValue;
            enemyController._hurtDirection = transform.position - collision.transform.position;
            enemyController._impulseSpeed = hurtSpeed;
            enemyController._moveBuffer = false;
            enemyController._isHurting = true;
            
        }
    }
}
