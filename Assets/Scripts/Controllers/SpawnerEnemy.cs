using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] Transform _grpEnemy;
    int waveCount = 0;
    bool isRunning = true;

    void Start()
    {
        if (isRunning == true)
        {
            StartCoroutine(Spawn(waveCount));
        }
    }

    void Update()
    {
        if(_grpEnemy.childCount == 0)
        {
            waveCount++;
            isRunning = true;
            Start();
        }
    }

    IEnumerator Spawn(int waveID)
    {
        foreach (var e in myWaves[waveID]._enemies)
        {
            isRunning = true;
            Vector2 spawnPos = (Vector2)transform.position;
            Instantiate(e, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(myWaves[waveID].spawnInterval);
            isRunning = false;
        }

    }
}
