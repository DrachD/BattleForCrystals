using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Nearest : MonoBehaviour
{
    [SerializeField] Character _character;

    [SerializeField] protected InfoPanel _infoPanel;

    protected float _distance;

    private NavMeshAgent _agent;
    
    private Spawner _spawner;

    private void Awake()
    {
        if (_infoPanel == null)
        {
            _infoPanel = GameObject.Find("Info Panel").GetComponent<InfoPanel>();
        }
        _agent = _character.GetComponent<NavMeshAgent>();
        _spawner = GetComponent<Spawner>();
    }

    // find the closest enemy
    protected IEnumerator GetClosestEnemy(Action action)
    {
        while (true)
        {
            float tmpDist = float.MaxValue;

            for (int i = 0; i < _spawner.spawnedObjects.Count; i++)
            {
                float minPathDistance = CalculatePathLength(_spawner.spawnedObjects[i].transform.position);

                if (tmpDist > minPathDistance)
                {
                    tmpDist = minPathDistance;
                }

                _distance = tmpDist;
                action?.Invoke();
            }

            yield return null;
        }
    }

    private float CalculatePathLength(Vector3 targetPosition)
    {
        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        if(_agent.enabled)
            _agent.CalculatePath(targetPosition, path);
        
        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        
        // The first point is the enemy's position.
        allWayPoints[0] = _character.gameObject.transform.position;
        
        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetPosition;
        
        // The points inbetween are the corners of the path.
        for(int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }
        
        // Create a float to store the path length that is by default 0.
        float pathLength = 0;
        
        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for(int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }
        
        return pathLength;
    }
}
