using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    [SerializeField] GameBoard gameBoard;
    [SerializeField] GameObject instructionsPanel;

    public char selectedLetter = '-';

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    public void ShowInstructions(bool show)
    {
        instructionsPanel.SetActive(show);
    }

    public void LetterCorrect(char letter)
    {

    }

    public void WordCorrect(string word)
    {

    }
}
