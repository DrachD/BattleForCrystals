using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] Text _bestRecortText;
    
    private IntVariable _scoreRecord;

    // display the best result at the end of the game
    private void OnEnable()
    {
        _scoreRecord = Resources.Load<IntVariable>("Save/ScoreRecord") as IntVariable;
        _bestRecortText.text = "best record: " + _scoreRecord.value.ToString();
    }
}
