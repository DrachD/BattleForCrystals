using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected Terrain _terrain;
    [SerializeField] protected InfoPanel _infoPanel;
    public List<GameObject> spawnedObjects = new List<GameObject>();

    private void Awake()
    {
        if (_terrain == null)
        {
            _terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        }

        if (_infoPanel == null)
        {
            _infoPanel = GameObject.Find("Info Panel").GetComponent<InfoPanel>();
        }
    }
}
