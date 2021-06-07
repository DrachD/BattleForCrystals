using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestCrystal : Nearest
{
    private void Start()
    {
        // find the closest crystal and set the distances for UI
        StartCoroutine(GetClosestEnemy(() => _infoPanel?.OnDistanceCrystalUpdateEvent(_distance)));
    }
}
