using TMPro;
using UnityEngine;

public class TileButton : MonoBehaviour
{
    [SerializeField] TMP_Text letterText;

    public char letter { get; private set; }

    public void SetLetter(char _letter)
    {
        letter = _letter;
        letterText.text = $"{letter}";
    }
}
