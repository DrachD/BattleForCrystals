using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestEnemy : Nearest
{
    private void Start()
    {
        // find the closest enemy and set the distances for UI
        StartCoroutine(GetClosestEnemy(() => _infoPanel?.OnDistanceEnemyUpdateEvent(_distance)));
    }
}
