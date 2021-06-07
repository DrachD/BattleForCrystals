using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CrystalSpawner _crystalSpawner;

    // enemy base damage
    [SerializeField] IntVariable _damage;

    public int Damage => _damage.value;

    private void Awake()
    {
        _crystalSpawner = GameObject.Find("Crystal Spawner").GetComponent<CrystalSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        // destroy the crystal and update UI
        if (obj.CompareTag("Crystal"))
        {
            _crystalSpawner.UpdateCrystalsUI(-1);
            _crystalSpawner.spawnedObjects.Remove(obj);
            Destroy(obj);
        }
    }
}
