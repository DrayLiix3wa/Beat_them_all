using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{

    public EnemyController enemyController;

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
        enemyController._attackAnimation = false;
    }


    public void EndHurt()
    {
        enemyController._hurtAnimation = false;
    }

    public void EndDeath()
    {
        enemyController._hurtAnimation = false;
    }
}
