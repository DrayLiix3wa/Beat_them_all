using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnnemyHitBoxManager : MonoBehaviour
{
    public string[] damageTags;
    public EnemyController enemyController;

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

            enemyController.damageTaken = collision.GetComponent<HitDamageValue>().hitDamageValue;
            enemyController._isHurting = true;
            
        }
    }
}
