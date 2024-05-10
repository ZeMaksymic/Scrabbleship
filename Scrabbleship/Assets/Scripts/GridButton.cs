using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GridButton : MonoBehaviour
{
    [SerializeField] TMP_Text letterText;

    public int row;
    public int column;
    public char letter;

    public bool isCorrectlyGuessed = false;

    public void SetData(Tuple<int,int> _coords, char _letter = ' ')
    {
        row = _coords.Item1;
        column = _coords.Item2;
        letter = _letter;
    }

    public void SetOnClick(UnityAction callback)
    {
        GetComponent<Button>().onClick.AddListener(delegate { callback?.Invoke(); });
    }

    public void SetYellow(char _letter)
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
        letterText.text = $"{_letter}";
    }

    public void SetOrange(char _letter)
    {
        GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.64f, 0.0f);
        letterText.text = $"{_letter}";
    }

    public void SetGreen(char _letter)
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.green;
        letterText.text = $"{_letter}";
    }

    public void SetGrey()
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        letterText.text = "";
    }

    public void reset(char _letter)
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.blue;
        letterText.text = "";
    }
}
