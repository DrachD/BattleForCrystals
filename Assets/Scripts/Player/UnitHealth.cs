using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] IntVariable health;

    [SerializeField] IntVariable maxHealth;

    [SerializeField] FloatVariable invulnerabilityTime;

    [SerializeField] InfoLife _infoLife;

    [SerializeField] GameOver _gameOver;

    public Action<int> OnChangeHealthEvent;

    private bool _invulnerable = false;

    private int _health;

    private int _maxHealth;

    private float _invulnerabilityTime;

    private float _time = 0.0f;

    private Character _character;

    private void Awake()
    {
        if (_infoLife == null)
        {
            _infoLife = GameObject.Find("Info Life").GetComponent<InfoLife>();
        }

        if (_gameOver == null)
        {
            _gameOver = GameObject.Find("GameOver").GetComponent<GameOver>();
        }

        OnChangeHealthEvent += _infoLife.ChangeHealth;

        _character = GetComponent<Character>();
        _health = health.value;
        _maxHealth = maxHealth.value;
        _invulnerabilityTime = invulnerabilityTime.value;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        // upon contact with the enemy, the enemy dies. We become temporarily invulnerable
        if (obj.CompareTag("Enemy") && !_invulnerable)
        {
            _invulnerable = true;
            Health(obj.GetComponent<Enemy>().Damage);
            StartCoroutine(TemporaryInvulnerability());
            _character.EnemySpawner.UpdateEnemiesUI(-1);
            _character.EnemySpawner.spawnedObjects.Remove(obj);
            Destroy(obj);
        }

        // replenish health to the maximum when picking up crystals
        if (obj.CompareTag("Crystal"))
        {
            _health = _maxHealth;
            OnChangeHealthEvent?.Invoke(_maxHealth);
        }
    }

    IEnumerator TemporaryInvulnerability()
    {
        yield return new WaitForSeconds(_invulnerabilityTime);
        _invulnerable = false;
    }

    // update the interface and call the end of the game on deaths
    private void Health(int value)
    {
        _health -= value;

        OnChangeHealthEvent?.Invoke(_health);

        if (_health <= 0)
        {
            Destroy(gameObject);
            _gameOver.gameObject.SetActive(true);
        }
    }
}
