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
    [SerializeField] GameObject youWinPanel;

    // MASTER LIST OF WORD PAIRS
    private List<List<string>> WORDS_LIST = new List<List<string>> {
        new List<string> {"HOP", "BASK"},
        new List<string> {"LID", "VEIL"},
        new List<string> {"JUG", "FERN"},
        new List<string> {"SAND", "LIPS"},
        new List<string> {"DART", "PELT"},
        new List<string> {"BALE", "WISP"},
        new List<string> {"COD", "VENT"},
        new List<string> {"BOLT", "GERM"},
        new List<string> {"RINK", "VIAL"},
        new List<string> {"YARN", "RUNG"}
    };
    private List<int> wordsListIndices = new List<int>();
    System.Random rando = new System.Random();

    private int wordCount = 0;
    private int guessCount = 0;

    private TileButton selectedTile;  // Tile that has been selected from letter bank
    public char selectedLetter { get { return selectedTile?.letter ?? ' '; }}

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        // Generate list of indices associated with WORDS_LIST
        for (int i = 0; i < WORDS_LIST.Count; i++) wordsListIndices.Add(i);
    }

    // Called when "PLAY" button is clicked
    public void NewGame()
    {
        //youWinPanel.SetActive(false);

        selectedTile = null;

        // Randomly pick a WORDS_LIST index, then remove it from play
        int gameIndex = wordsListIndices[rando.Next(wordsListIndices.Count)];
        wordsListIndices.Remove(gameIndex);

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

        AudioManager.Instance.PlayClick();
    }

    public void LetterGuessed(bool isCorrect)
    {
        EventSystem.current.SetSelectedGameObject(null);  // Deselect tile

        guessCount += 1;
        UpdateGuessCount();
        
        if (isCorrect) letterBank.DestroyTile(selectedTile);  // Remove successful tile from letter bank

        selectedTile = null;

        AudioManager.Instance.PlayClick();
    }

    public void WordCorrect(string word)
    {
        wordCount -= 1;
        UpdateWordCount();

        if (wordCount == 0)
        {
            Debug.Log("YOU WIN!!!");
            
            //youWinPanel.SetActive(true);  // youWinPanel is being destroyed for some reason???
        }

        AudioManager.Instance.PlayBoom();
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
