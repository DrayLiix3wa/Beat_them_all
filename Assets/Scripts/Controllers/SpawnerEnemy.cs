using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Wave
{
    public GameObject[] _enemies;
    public string word;
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
            if (gameObject.transform.childCount == 0)
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

    IEnumerator Spawn(int waveID)
    {
        foreach (var e in myWaves[waveID]._enemies)
        {
            isRunning = true;
            Vector2 spawnPos = (Vector2)transform.position;
            GameObject enemy = Instantiate(e, spawnPos, Quaternion.identity);
            enemy.transform.parent = transform;
            yield return new WaitForSeconds(myWaves[waveID].spawnInterval);
            isRunning = false;
        }

    }
}
