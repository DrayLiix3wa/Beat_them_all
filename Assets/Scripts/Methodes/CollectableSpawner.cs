using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] 
    private List<SO_objectPool> _collectablePrefabs;

    public PoolsManager poolManager;
    private GameObject _gameObject;

    public SO_player playerStats;
    public SO_objectPool staminaPool;
    public SO_objectPool healthPool;

    public void SpawnCollectable(Vector2 position)
    {
        if(playerStats.health <= 50f && playerStats.stamina > 50f)
        {
            int randomNumber = Random.Range(0, 10);

            if (randomNumber <= 7)
            {
                _gameObject = poolManager.GetObjectFromPool(healthPool.poolName);
                _gameObject.transform.position = position;
            }
            else
            {
                _gameObject = poolManager.GetObjectFromPool(staminaPool.poolName);
                _gameObject.transform.position = position;
            }
        }
        else if(playerStats.stamina <= 50f && playerStats.health > 50f)
        {
            int randomNumber = Random.Range(0, 10);

            if (randomNumber <= 7)
            {
                _gameObject = poolManager.GetObjectFromPool(staminaPool.poolName);
                _gameObject.transform.position = position;
            }
            else
            {
                _gameObject = poolManager.GetObjectFromPool(healthPool.poolName);
                _gameObject.transform.position = position;
            }
        }
        else
        {
            int index = Random.Range(0, _collectablePrefabs.Count);
            var selectedCollectable = _collectablePrefabs[index];

            _gameObject = poolManager.GetObjectFromPool(selectedCollectable.poolName);
            _gameObject.transform.position = position;
        }
    }
}
