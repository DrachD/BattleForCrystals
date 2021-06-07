using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InfoView : MonoBehaviour
{
    public Image icon;

    public Text text;

    private string _baseText;

    public InfoType InfoType;

    private void OnValidate()
    {
        icon = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
    }

    public void Init(string baseText)
    {
        _baseText = baseText;
        text.text = _baseText;
    }

    public void UpdateValue(int value)
    {
        text.text = _baseText + value.ToString();
    }

    public void UpdateValue(float value)
    {
        text.text = _baseText + Math.Round(value, 2).ToString();
    }

    public void UpdateText(string str)
    {
        text.text = _baseText + str;
    }
}

public enum InfoType
{
    COUNT_CRYSTALS,
    COUNT_ENEMIES,
    CRYSTAL_DIST,
    ENEMY_DIST,
}