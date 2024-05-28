using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public SO_objectPool[] _enemies;
    public float spawnInterval;
}

public class SpawnerEnemy : MonoBehaviour
{
    public Wave[] myWaves;
    public bool timerOnOff = false;
    public float _timeVague;
    int waveCount = 0;
    bool isRunning = true;
    float _chrono = 0f;
    public PoolsManager poolsManager;

    void Start()
    {
        if (isRunning == true && waveCount < myWaves.Length)
        {
            StartCoroutine(Spawn(waveCount));
        }
    }

    void Update()
    {
        _chrono += Time.deltaTime;

        if ( timerOnOff == false )
        {
            if ( CheckEnemieCount() == 0 )
            {
                waveCount++;
                if( waveCount < myWaves.Length )
                {
                    isRunning = true;
                    Start();
                }
                else { 
                    isRunning = false; 
                }
                
            }
        }
        else
        {
            if ( _chrono >= _timeVague )
            {
                waveCount++;
                if ( waveCount < myWaves.Length )
                {
                    isRunning = true;
                    Start();
                    _chrono = 0f;
                }
                else 
                { 
                    isRunning = false; 
                    _chrono = 0f;
                }
            }
        }
    }

    int CheckEnemieCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Count(e => e.activeInHierarchy);
    }

    IEnumerator Spawn(int waveID)
    {
        foreach (var e in myWaves[waveID]._enemies)
        {
            isRunning = true;
            Vector2 spawnPos = (Vector2)transform.position;
            GameObject enemie = poolsManager.GetObjectFromPool(e.poolName);
            enemie.transform.position = spawnPos;

            yield return new WaitForSeconds(myWaves[waveID].spawnInterval);
        }

        isRunning = false;

    }
}
