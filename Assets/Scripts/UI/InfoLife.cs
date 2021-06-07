using System;
using UnityEngine;
using UnityEngine.UI;

public class InfoLife : MonoBehaviour
{
    private Character _character;

    private Text _text;

    private string _baseText;

    private void Awake()
    {
        _baseText = "";
        _character = GameObject.Find("Character").GetComponent<Character>();
        _text = GetComponentInChildren<Text>();
    }

    public void ChangeHealth(int value)
    {
        _text.text = _baseText + value.ToString();
    }
}
