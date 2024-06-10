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
    [Space(10)]

    [Header("Set Timer To Win")]
    public bool setTimerToWin = false;
    public SO_objectPool enemieToSpawnOnTimer;
    public float spawnInterval = 15f;

    [Space(10)]
    int waveCount = 0;
    bool isRunning = true;
    public PoolsManager poolsManager;

    public GameObject[] spawnerSpots;
    
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

        if (isRunning == true && setTimerToWin == true)
        {
            StartCoroutine(SpawnEnemiesCoroutine());
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

            Vector2 spawnPos;

            if (spawnerSpots.Length > 0)
            {
                int randomSpot = Random.Range(0, spawnerSpots.Length);
                 spawnPos = (Vector2)spawnerSpots[randomSpot].transform.position;
            }else
            {
                 spawnPos = (Vector2)transform.position;
            }

            GameObject enemie = poolsManager.GetObjectFromPool(e.poolName);
            enemie.transform.position = spawnPos;

            yield return new WaitForSeconds(myWaves[waveID].spawnInterval);
        }

        isRunning = false;

    }


    IEnumerator SpawnEnemiesCoroutine()
    {
        int maxEnemies = 100;
        int count = 0; 

        while (count < maxEnemies)
        {
            for (int i = 0; i < 5 && count < maxEnemies; i++)
            {
                Vector2 spawnPos;

                if (spawnerSpots != null && spawnerSpots.Length > 0)
                {
                    int randomSpot = Random.Range(0, spawnerSpots.Length);
                    spawnPos = (Vector2)spawnerSpots[randomSpot].transform.position;
                }
                else
                {
                    spawnPos = (Vector2)transform.position; 
                }

                GameObject enemy = poolsManager.GetObjectFromPool(enemieToSpawnOnTimer.poolName); 
                if (enemy != null) 
                {
                    enemy.transform.position = spawnPos; 
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }


    private void OnValidate()
    {
        if (level != null)
        {
            _timeVague = level.timeToWin;

        }
    }


}
