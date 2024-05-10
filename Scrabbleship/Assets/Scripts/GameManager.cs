using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    [SerializeField] LetterBank letterBank;
    [SerializeField] GameBoard gameBoard;
    [SerializeField] GameObject instructionsPanel;

    // MASTER LIST OF WORD PAIRS
    private List<List<string>> WORDS_LIST = new List<List<string>> {
        new List<string> {"DOG", "BARK"},
        new List<string> {"CAT", "MEOW"},
        new List<string> {"BIRD", "PEEP"}
    };

    int gameIndex = -1;  // Incremented with each new game

    private TileButton selectedTile;  // Tile that has been selected from letter bank
    public char selectedLetter { get { return selectedTile.letter; }}

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
        gameIndex += 1;

        List<string> words = WORDS_LIST[gameIndex];

        Debug.Log($"NewGame: words = {words[0]}, {words[1]}");

        gameBoard.SetupBoard(words);  // Assign words locations on the grid

        // Extract letters from words
        List<char> letters = new List<char>();
        foreach (string word in words)
            foreach (char letter in word)
                letters.Add(letter);
        
        letters.Sort();  // Sort list alphabetically

        Debug.Log("NewGame: letters = " + String.Join(", ", letters.ToArray()));

        letterBank.SetLetters(letters);
    }

    public void LetterTileSelected(TileButton button)
    {
        selectedTile = button;

        Debug.Log("LetterTileSelected: " + selectedTile.letter);
    }

    public void LetterCorrect(char letter)
    {

    }

    public void WordCorrect(string word)
    {

    }

    public void ShowInstructions(bool show)
    {
        instructionsPanel.SetActive(show);
    }
}
