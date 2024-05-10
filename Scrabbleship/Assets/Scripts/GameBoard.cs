using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine; 

public class GameBoard : MonoBehaviour
{
    private List<string> words;
	private List<Tuple<int,int>> wordStartCoordinates = new List<Tuple<int,int>>();

    private List<List<GridButton>> wordButtons =new List<List<GridButton>>();

    // Called by GameManager when the "PLAY" button is clicked
    public void SetupBoard(List<string> _words)
    {
        Debug.Log("IN SETUP BOARD");
        words = _words;
        // wordButtons = _wordButtons;

        System.Random rnd = new System.Random();
        int index = 0;
        while (true) {
            Debug.Log("index: " + index);
            string word = words[index];
            int row = rnd.Next(1, 7);
            int col = rnd.Next(1, 7 - word.Length + 1);
            Debug.Log("word: " + word);
            Debug.Log("row: " + row);
            Debug.Log("col: " + col);

            Tuple<int,int> wordStartCoordinate = new Tuple<int,int>(row, col);
            
            if (doesWordAlreadyExist(wordStartCoordinate) == null) {
                wordStartCoordinates.Add(wordStartCoordinate);
                index++;
            }
            
            if (index >= words.Count) return;
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
        if (letter == button.letter) {
            // change tile to green
            GameManager.Instance.LetterGuessed(true);

            // string word = getWordAtCoordinate(coordinate);
            // int index = words.IndexOf(word);
            // bool wordCorrect = true;
            // foreach(GridButton gridbutton in wordButtons[index]) {
            //     if (!gridbutton.isCorrectlyGuessed) {
            //         wordCorrect = false;
            //         return;
            //     }
            // }
            // if (wordCorrect) GameManager.Instance.WordCorrect(word);
        }
        else {
            string word = getWordAtCoordinate(coordinate);
            if (word != "") {
                if (word.Contains(letter)) {
                     // change tile to yellow
                }
                else {
                    // change tile to grey
                }
            }
        }
    }

	private Tuple<int,int>? doesWordAlreadyExist(Tuple<int,int> coordinate) {
        Debug.Log("IN DOES WORD ALREADY EXIST");
        Debug.Log("coordinate: " + coordinate);
		for (int i = 0; i < wordStartCoordinates.Count; i++) {
            int wordLength = words[i].Length;
            Tuple<int,int> startCoordinate = wordStartCoordinates[i];
            if (startCoordinate.Item1 == coordinate.Item1) {
                for (int j = 0; j < wordLength; j++) {
                    if (coordinate.Item2 == startCoordinate.Item2 + j) return startCoordinate;
                }
            }
		}
        return null;
	}

    private string getWordAtCoordinate(Tuple<int,int> coordinate) {
        Debug.Log("IN GET WORD AT COORDINATE");
        Debug.Log("coordinate: " + coordinate);
        Tuple<int,int>? wordStartCoordinate = doesWordAlreadyExist(coordinate);
        if (wordStartCoordinate != null) {
            int index = wordStartCoordinates.IndexOf(wordStartCoordinate);
            return words[index];
        }
        return "";
    }
}