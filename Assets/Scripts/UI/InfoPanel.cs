using UnityEngine;
using System;

// main dashboard with events
public class InfoPanel : MonoBehaviour
{
    public Action<int> OnCountCrystalsUpdateEvent;
    public Action<int> OnCountEnemiesUpdateEvent;
    public Action<float> OnDistanceCrystalUpdateEvent;
    public Action<float> OnDistanceEnemyUpdateEvent;

    [SerializeField] InfoView[] _infoViews;

    private void OnValidate()
    {
        _infoViews = GetComponentsInChildren<InfoView>();

        for (int i = 0; i < _infoViews.Length; i++)
        {
            switch (_infoViews[i].InfoType)
            {
                case InfoType.COUNT_CRYSTALS:
                    _infoViews[i].Init("");
                    break;
                case InfoType.COUNT_ENEMIES:
                    _infoViews[i].Init("");
                    break;
                case InfoType.CRYSTAL_DIST:
                    _infoViews[i].Init("dist: ");
                    break;
                case InfoType.ENEMY_DIST:
                    _infoViews[i].Init("dist: ");
                    break;
            }
        }
    }

    // subscribe to methods depending on the type of information
    private void Awake()
    {
        for (int i = 0; i < _infoViews.Length; i++)
        {
            switch (_infoViews[i].InfoType)
            {
                case InfoType.COUNT_CRYSTALS:
                    OnCountCrystalsUpdateEvent += _infoViews[i].UpdateValue;
                    break;
                case InfoType.COUNT_ENEMIES:
                    OnCountEnemiesUpdateEvent += _infoViews[i].UpdateValue;
                    break;
                case InfoType.CRYSTAL_DIST:
                    OnDistanceCrystalUpdateEvent += _infoViews[i].UpdateValue;
                    break;
                case InfoType.ENEMY_DIST:
                    OnDistanceEnemyUpdateEvent += _infoViews[i].UpdateValue;
                    break;
            }
        }
    }

    // We deactivate it when creating a game, 
    // otherwise the subscription will not work and the application will generate an error
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
