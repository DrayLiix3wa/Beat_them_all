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
    public bool setTimerToWin = false;
    int waveCount = 0;
    bool isRunning = true;
    public PoolsManager poolsManager;
    
    public SO_Level level;

    private float _timeVague;

    void Start()
    {
        if (level != null)
        {
            _timeVague = level.timeToWin;
        }

        if (isRunning == true && waveCount < myWaves.Length)
        {
            StartCoroutine(Spawn(waveCount));
        }
    }

    void Update()
    {
        if ( setTimerToWin == false )
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
            if (CheckEnemieCount() == 0)
            {
                waveCount++;
                if (waveCount < myWaves.Length)
                {
                    isRunning = true;
                    Start();
                }
                else
                {
                    isRunning = false;
                }

            }

            if (waveCount == 0 && level.chrono < _timeVague)
            {
                waveCount = 0;
                Start();
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

    private void OnValidate()
    {
        if (level != null)
        {
            _timeVague = level.timeToWin;

        }
    }


}
