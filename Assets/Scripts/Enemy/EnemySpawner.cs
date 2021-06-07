using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] Transform[] _points;

    [SerializeField] GameObject _enemyPrefab;
    
    [SerializeField] FloatVariable spawnInterval;
    
    [SerializeField] IntVariable maxEnemies;
    
    private int _maxEnemies;

    private float _spawnInterval;

    private int _countEnemies = 0;


    private void Awake()
    {
        _maxEnemies = maxEnemies.value;
        _spawnInterval = spawnInterval.value;
    }

    private void Start()
    {
        StartCoroutine(SpawnOnPoint(_points[0], _enemyPrefab));
    }

    // we spawn enemies at a certain point
    IEnumerator SpawnOnPoint(Transform spawnPoint, GameObject prefab)
    {
        while (true)
        {
            if (_countEnemies < _maxEnemies)
            {
                yield return SpawnAllEnemies(spawnPoint, prefab);
            }
            else
            {
                yield return null;
            }
        }
    }


    // we spawn all enemies at a certain point
    IEnumerator SpawnAllEnemies(Transform spawnPoint, GameObject prefab)
    {
        for (int i = _countEnemies; i < _maxEnemies; i++)
        {
            GameObject obj = Instantiate(_enemyPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
            spawnedObjects.Add(obj);
            obj.transform.SetParent(transform);

            UpdateEnemiesUI(1);

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    // update data UI
    public void UpdateEnemiesUI(int value)
    {
        _countEnemies += value;

        _infoPanel?.OnCountEnemiesUpdateEvent(_countEnemies);
    }
}
