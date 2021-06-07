using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrystalSpawner : Spawner
{
    [SerializeField] GameObject _crystalPrefab;

    [SerializeField] IntVariable initialCountCrystals;

    [SerializeField] IntVariable maxCrystals;

    [SerializeField] FloatVariable spawnInterval;
    
    private float _spawnInterval;

    private int _initialCountCrystals;

    private int _maxCrystals;

    private float _width;

    private float _length;

    private int _countCrystals = 0;

    private void Awake()
    {
        _spawnInterval = spawnInterval.value;
        _maxCrystals = maxCrystals.value;
        _initialCountCrystals = initialCountCrystals.value;
        _width = _terrain.terrainData.size.x;
        _length = _terrain.terrainData.size.z;
    }

    private IEnumerator Start()
    {
        StartCoroutine(InitialSpawnCrystal());
        yield return null;
        StartCoroutine(SpawnCrystal());
    }

    // spawns a certain amount of crystals at the beginning of the game
    IEnumerator InitialSpawnCrystal()
    {
        for (int i = 0; i < _initialCountCrystals; i++)
        {
            RandomSpawn(_crystalPrefab);
        }

        yield return null;
    }

    // spawns crystals on a permanent basis
    IEnumerator SpawnCrystal()
    {
        while (true)
        {
            while (_countCrystals < _maxCrystals)
            {
                yield return new WaitForSeconds(_spawnInterval);

                RandomSpawn(_crystalPrefab);
            }

            yield return null;
        }
    }

    private void RandomSpawn(GameObject prefab)
    {
        // Let's set a random plot for crystals to spawn
        float x = UnityEngine.Random.Range(0, _width);
        float z = UnityEngine.Random.Range(0, _length);

        RaycastHit hit;

        // casts a beam to spawn a crystal outside the obstacle
        if (Physics.Raycast(_terrain.transform.position + new Vector3(x, _terrain.transform.position.y, z), transform.TransformDirection(Vector3.up), out hit))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                return;
            }
        }

        // Spawning Crystals Outside the obstacle
        if (!Physics.CheckSphere(new Vector3(x, prefab.transform.localScale.y, z), .6f))
        {
            UpdateCrystalsUI(1);
            
            GameObject obj = Instantiate(prefab, new Vector3(x, prefab.transform.localScale.y * 0.5f, z), Quaternion.identity) as GameObject;
            spawnedObjects.Add(obj);
            obj.transform.SetParent(transform);
        }
    }

    // update the crystal counter in the panel
    public void UpdateCrystalsUI(int value)
    {
        _countCrystals += value;

        _infoPanel?.OnCountCrystalsUpdateEvent(_countCrystals);
    }
}
