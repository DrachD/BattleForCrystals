using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AnimationStateController))]
public class Character : MonoBehaviour
{
    public Action<int> OnChangeScoreEvent;

    [SerializeField] Camera _camera;

    [SerializeField] FloatVariable _moveSpeed;

    [SerializeField] InfoScore _infoScore;

    [SerializeField] InfoPanel _infoPanel;
    
    private NavMeshAgent _myAgent;

    private AnimationStateController _animationStateController;

    private EnemySpawner _enemySpawner;
    public EnemySpawner EnemySpawner => _enemySpawner;

    private CrystalSpawner _crystalSpawner;

    private Vector3 newPosition;

    public LayerMask whatCanBeClickedOn;

    private int _countCrystals = 0;
    private int _countCrystalsUI = 0;

    private IntVariable _scoreRecord;

    private CameraController _cameraController;

    private void Awake()
    {
        if (_infoScore == null)
        {
            _infoScore = GameObject.Find("Info Score").GetComponent<InfoScore>();
        }

        if (_infoPanel == null)
        {
            _infoPanel = GameObject.Find("Info Panel").GetComponent<InfoPanel>();
        }

        OnChangeScoreEvent += _infoScore.ChangeScore;
        OnChangeScoreEvent.Invoke(_countCrystals);
        _scoreRecord = Resources.Load<IntVariable>("Save/ScoreRecord") as IntVariable;
        _crystalSpawner = GameObject.Find("Crystal Spawner").GetComponent<CrystalSpawner>();
        _enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
        _myAgent = GetComponent<NavMeshAgent>();
        _cameraController = _camera.GetComponent<CameraController>();
        _animationStateController = GetComponent<AnimationStateController>();
    }

    private void Start()
    {
        newPosition = transform.position;
        _myAgent.speed = _moveSpeed.value;
    }

    private void Update()
    {
        // move the camera behind our player
        _camera.transform.position = new Vector3(transform.position.x + _cameraController.distanceBetweenObjectsX, 
                                                 transform.position.y + _cameraController.distanceBetweenObjectsY, 
                                                 transform.position.z + _cameraController.distanceBetweenObjectsZ);

        Vector3 between;
        
        InputHandler();

        between = newPosition - transform.position;

        // changing the state of the animation
        _animationStateController.AnimatorRunFloat(between.magnitude);
        _animationStateController.OnAnimatorMove();
    }

    private void InputHandler()
    {
        // move the character in the direction of the click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, whatCanBeClickedOn))
            {
                _myAgent.SetDestination(hit.point);
                newPosition = hit.point;
            }
        }

        // call the panel tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_infoPanel.gameObject.active)
            {
                _infoPanel.gameObject.SetActive(false);
            }
            else
            {
                _infoPanel.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        // we add points for the selection of crystals and destroy them
        if (obj.CompareTag("Crystal"))
        {
            _countCrystals += obj.GetComponent<Crystal>().Score;
            
            OnChangeScoreEvent.Invoke(_countCrystals);
            _crystalSpawner.UpdateCrystalsUI(-1);
            _crystalSpawner.spawnedObjects.Remove(obj);
            Destroy(obj);
        }
    }

    // Save the record for the number of crystals
    private void OnDisable()
    {
        if (_scoreRecord.value < _countCrystals)
        {
            _scoreRecord.value = _countCrystals;
        }
    }
}
