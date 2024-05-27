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
    int waveCount;

    void Start()
    {
        // Je spawn la premiere vague du tableau
        StartCoroutine(Spawn(waveCount));
    }

    void Update()
    {

    }

    IEnumerator Spawn(int waveID)
    {
        foreach (var e in myWaves[waveID]._enemies)
        {
            Vector2 spawnPos = (Vector2)transform.position;
            Instantiate(e, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(myWaves[waveID].spawnInterval);
        }

    }
}
