using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileButton : MonoBehaviour
{
    [SerializeField] TMP_Text letterText;

    public char letter { get; private set; }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { GameManager.Instance.LetterTileSelected(this); });
    }

    public void SetLetter(char _letter)
    {
        letter = _letter;
        letterText.text = $"{letter}";
    }
}
