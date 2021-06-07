using UnityEngine;
using UnityEngine.UI;

public class InfoScore : MonoBehaviour
{
    private Text _text;

    private string _baseText;

    private void Awake()
    {
        _baseText = "score: ";
        _text = GetComponentInChildren<Text>();
    }

    public void ChangeScore(int value)
    {
        _text.text = _baseText + value.ToString();
    }
}
