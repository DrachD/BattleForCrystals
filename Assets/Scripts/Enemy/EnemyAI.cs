using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] FloatVariable _moveSpeed;
    private NavMeshAgent _agent;

    public LayerMask whatIsGround;
    public LayerMask whatIsObstacle;

    // Patroling
    public Vector3 walkPoint;
    public float walkPointRange;
    private bool _walkPointSet;
    private bool _obstaclePointSet;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.speed = _moveSpeed.value;
    }

    private void Update()
    {
        Patroling();
    }

    // our enemy is patrolling in a random direction
    private void Patroling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet && !_obstaclePointSet)
        {
            _agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            _walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            _walkPointSet = true;
        }
        
        if (Physics.Raycast(walkPoint, transform.up, 2f, whatIsObstacle))
        {
            _obstaclePointSet = true;
        }
        else
        {
            _obstaclePointSet = false;
        }
    }
}
