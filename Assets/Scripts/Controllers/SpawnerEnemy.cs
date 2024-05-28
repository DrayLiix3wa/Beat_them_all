using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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
        if (isRunning == true)
        {
            StartCoroutine(Spawn(waveCount));
        }
    }

    void Update()
    {
        _chrono += Time.deltaTime;

        if (timerOnOff == false)
        {
            if (CheckEnemieCount() == 0)
            {
                waveCount++;
                isRunning = true;
                Start();
            }
        }
        else
        {
            if (_chrono >= _timeVague)
            {
                waveCount++;
                isRunning = true;
                Start();
                _chrono = 0f;
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
            //GameObject enemy = Instantiate(e, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(myWaves[waveID].spawnInterval);
            isRunning = false;
        }

    }
}
