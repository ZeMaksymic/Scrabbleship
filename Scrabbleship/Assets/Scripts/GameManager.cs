using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    [SerializeField] LetterBank letterBank;
    [SerializeField] TMP_Text wordCountText;
    [SerializeField] TMP_Text guessCountText;
    [SerializeField] GameBoard gameBoard;
    [SerializeField] GameObject instructionsPanel;

    // MASTER LIST OF WORD PAIRS
    private List<List<string>> WORDS_LIST = new List<List<string>> {
        new List<string> {"DOG", "BARK"},
        new List<string> {"CAT", "MEOW"},
        new List<string> {"BIRD", "PEEP"}
    };

    private int gameIndex = -1;  // Incremented with each new game
    private int wordCount = 0;
    private int guessCount = 0;

    private TileButton selectedTile;  // Tile that has been selected from letter bank
    public char selectedLetter { get { return selectedTile?.letter ?? '-'; }}

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    // Called when "PLAY" button is clicked
    public void NewGame()
    {
        selectedTile = null;

        gameIndex += 1;

        List<string> words = WORDS_LIST[gameIndex];
        Debug.Log($"NewGame: words = {words[0]}, {words[1]}");

        wordCount = words.Count;
        UpdateWordCount();

        gameBoard.SetupBoard(words);  // Assign words locations on the grid

        // Extract letters from words
        List<char> letters = new List<char>();
        foreach (string word in words)
            foreach (char letter in word)
                letters.Add(letter);
        
        letters.Sort();  // Sort list alphabetically

        Debug.Log("NewGame: letters = " + String.Join(", ", letters.ToArray()));

        letterBank.SetLetters(letters);

        UpdateGuessCount();
    }

    public void LetterTileSelected(TileButton button)
    {
        selectedTile = button;

        Debug.Log("LetterTileSelected: " + selectedTile.letter);
    }

    public void LetterGuessed(bool isCorrect)
    {
        EventSystem.current.SetSelectedGameObject(null);  // Deselect tile

        guessCount += 1;
        UpdateGuessCount();
        
        if (isCorrect) letterBank.DestroyTile(selectedTile);  // Remove successful tile from letter bank
    }

    public void WordCorrect(string word)
    {
        wordCount -= 1;
        UpdateWordCount();

        // TODO: If wordCount == 0, you win!!!
    }

    private void UpdateWordCount()
    {
        wordCountText.text = wordCount.ToString();
    }

    private void UpdateGuessCount()
    {
        guessCountText.text = guessCount.ToString();
    }

    public void ShowInstructions(bool show)
    {
        instructionsPanel.SetActive(show);
    }
}
