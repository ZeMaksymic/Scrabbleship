using System;
using System.Collections.Generic;
using UnityEngine; 

public class GameBoard : MonoBehaviour
{
    private List<string> words;
	private List<Tuple<int,int>> wordStartCoordinates = new List<Tuple<int,int>>();

    private List<List<GridButton>> wordButtons =new List<List<GridButton>>();

    [SerializeField] GridButton gridButtonPrefab;

    public const int GRID_SIZE = 6;

    // Called by GameManager when the "PLAY" button is clicked
    public void SetupBoard(List<string> _words)
    {
        Debug.Log("IN SETUP BOARD");
        words = _words;
        foreach(string word in words) {
            wordButtons.Add(new List<GridButton>());
        }

        System.Random rnd = new System.Random();
        int index = 0;
        while (true) {
            // Debug.Log("index: " + index);
            string word = words[index];
            int row = rnd.Next(0, GRID_SIZE - word.Length + 1);
            int col = rnd.Next(0, GRID_SIZE);
            // Debug.Log("word: " + word);
            // Debug.Log("row: " + row);
            // Debug.Log("col: " + col);

            Tuple<int,int> wordStartCoordinate = new Tuple<int,int>(col, row);
            
            if (doesWordAlreadyExist(wordStartCoordinate, word.Length) == null) {
                wordStartCoordinates.Add(wordStartCoordinate);
                index++;
            }
            
            if (index >= words.Count) {
                SetGridTiles();
                return;
            }
	    }
    }

    public void SetGridTiles()
    {
        // Debug.Log("IN SET GRID TILES");
        // Remove any existing tiles
        foreach (Transform child in transform) Destroy(child.gameObject);
        transform.DetachChildren();

        // Create and position a tile for each letter
        for (int i = 0; i < GRID_SIZE; i++) // col
        {
            for (int j = 0; j < GRID_SIZE; j++) { //row
                // Debug.Log("col: " + i);
                // Debug.Log("row: " + j);

                GridButton gridButton = Instantiate(gridButtonPrefab, transform);
                Tuple<int,int> coords = new Tuple<int,int>(i,j);
                char letter = getCoordinatesLetter(coords);
                gridButton.reset(letter);
                if (letter != ' ') addButtonToWord(gridButton, coords);
                gridButton.SetData(coords, letter);
                gridButton.SetOnClick(callback: () => { MakeGuess(gridButton); });

                if (j < GRID_SIZE)
                {
                    float xPos = (j * 100f);
                    float yPos = -10f - (i * 100f);
                    gridButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
                }
            }
        }
    }

    // Called when the user clicks on a grid button
    public void MakeGuess(GridButton button)
    {
        // The letter that the user has clicked on in the letter bank
        char letter = GameManager.Instance.selectedLetter;

        // Grab row/column from the GridButton that was tapped
        int row = button.row;
        int column = button.column;

        Tuple<int,int> coordinate = new Tuple<int,int>(row, column);
        if (button.letter == ' ' || button.letter == '-')
        {
            button.SetGrey();
            GameManager.Instance.LetterGuessed(false);
        }
        else if (letter == button.letter) {
            // change tile to green
            GameManager.Instance.LetterGuessed(true);

            button.SetGreen(letter);

            button.isCorrectlyGuessed = true;

            // TODO: If letter is in an existing Yellow tile, remove it (?)

            string word = getWordAtCoordinate(coordinate);
            int index = words.IndexOf(word);
            bool wordCorrect = true;
            foreach(GridButton gridbutton in wordButtons[index]) {
                if (!gridbutton.isCorrectlyGuessed) {
                    wordCorrect = false;
                    return;
                }
            }
            if (wordCorrect) GameManager.Instance.WordCorrect(word);
        }
        else {
            string word = getWordAtCoordinate(coordinate);
            if (word != "")
                if (word.Contains(letter))
                {
                    button.SetYellow(letter);
                    GameManager.Instance.LetterGuessed(false);
                }
                else
                {
                    button.SetOrange(letter);
                    GameManager.Instance.LetterGuessed(false);
                }
        }
    }

	private Tuple<int,int>? doesWordAlreadyExist(Tuple<int,int> coordinate, int wordLength1) {
        Debug.Log("IN DOES WORD ALREADY EXIST");
        Debug.Log("coordinate: " + coordinate);
		for (int i = 0; i < wordStartCoordinates.Count; i++) {
            Debug.Log("i: " + i);
            int wordLength2 = words[i].Length;
            Debug.Log("wordLength2: " + wordLength2);
            Tuple<int,int> startCoordinate = wordStartCoordinates[i];
            Debug.Log("startCoordinate: " + startCoordinate);
            if (startCoordinate.Item1 == coordinate.Item1) {
                for (int j = 0; j < wordLength2; j++) {
                    Debug.Log("j: " + j);
                    if (coordinate.Item2 + wordLength1 >= startCoordinate.Item2 + j
                        || coordinate.Item2 <= startCoordinate.Item2 + j ) return startCoordinate;
                }
            }
		}
        return null;
	}

    private string getWordAtCoordinate(Tuple<int,int> coordinate) {
        // Debug.Log("IN GET WORD AT COORDINATE");
        // Debug.Log("coordinate: " + coordinate);
        for (int i = 0; i < wordStartCoordinates.Count; i++) {
            int wordLength = words[i].Length;
            Tuple<int,int> startCoordinate = wordStartCoordinates[i];
            if (startCoordinate.Item1 == coordinate.Item1) {
                for (int j = 0; j < wordLength; j++) {
                    if (coordinate.Item2 == startCoordinate.Item2 + j) return words[i];
                }
            }
        }
        return "";
    }

    private char getCoordinatesLetter(Tuple<int,int> coordinate) {
        // Debug.Log("IN GET WORD AT COORDINATE");
        // Debug.Log("coordinate: " + coordinate);
        string word = getWordAtCoordinate(coordinate);
        // Debug.Log("word: " + word);
        if (word == "") return ' ';
        int index = words.IndexOf(word);
        // Debug.Log("index: " + index);
        Tuple<int,int> startCoordinate = wordStartCoordinates[index];
        // Debug.Log("startCoordinate: " + startCoordinate);
        // Debug.Log("word[coordinate.Item2 - startCoordinate.Item2]: " + word[coordinate.Item2 - startCoordinate.Item2]);
        return word[coordinate.Item2 - startCoordinate.Item2];
    }

    private void addButtonToWord(GridButton gridButton, Tuple<int,int> coordinate) {
        string word = getWordAtCoordinate(coordinate);
        if (word != "") {
            int index = words.IndexOf(word);
            wordButtons[index].Add(gridButton);
        }
    }
}