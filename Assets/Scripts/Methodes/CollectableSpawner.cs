using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] 
    private List<SO_objectPool> _collectablePrefabs;

    public PoolsManager poolManager;
    private GameObject _gameObject;

    public void SpawnCollectable(Vector2 position)
    {
        int index = Random.Range(0, _collectablePrefabs.Count);
        var selectedCollectable = _collectablePrefabs[index];

        _gameObject = poolManager.GetObjectFromPool(selectedCollectable.poolName);
        _gameObject.transform.position = position;
    }
}
