using System;
using TMPro;
using UnityEngine;

public class GridButton : MonoBehaviour
{
    [SerializeField] TMP_Text letterText;

    public int row;
    public int column;
    public char letter;

    public void SetData(Tuple<int,int> _coords, char _letter = ' ')
    {
        row = _coords.Item1;
        column = _coords.Item2;
        letter = _letter;
    }

    public void SetYellow(char _letter)
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
        letterText.text = $"{_letter}";
    }

    public void SetGreen(char _letter)
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.green;
        letterText.text = $"{_letter}";
    }
}
