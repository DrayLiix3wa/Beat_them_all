using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChronoSpawnerEnemy : MonoBehaviour
{

    [Header("Set Timer To Win")]
    public bool setTimerToWin = false;
    public SO_objectPool enemieToSpawnOnTimer;
    public float spawnInterval = 15f;

    [Space(10)]
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


        if (setTimerToWin == true)
        {
            StartCoroutine(SpawnEnemiesCoroutine());
        }
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        int maxEnemies = 100;
        int count = 0;

        int enemieWaveCount = 4;

        while (count < maxEnemies)
        {
            for (int i = 0; i < enemieWaveCount && count < maxEnemies; i++)
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
                count++;
            }

            enemieWaveCount++;
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
