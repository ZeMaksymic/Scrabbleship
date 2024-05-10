using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private List<string> words;

    // Called by GameManager when the "PLAY" button is clicked
    public void SetupBoard(List<string> _words)
    {
        words = _words;

        // TODO: Randomly place words horizontally or vertically in game board tiles
    }

    // Called when the user clicks on a grid button
    public void MakeGuess(GridButton button)
    {
        // The letter that the user has clicked on in the letter bank
        char letter = GameManager.Instance.selectedLetter;

        // Grab row/column from the GridButton that was tapped
        int row = button.row;
        int column = button.column;

        // TODO: Logic check
        // If the letter is in the correct location – GREEN
        // If the letter hit the right word, but in the wrong location – YELLOW
        // If the letter didn't hit anything - GREY

        // If a letter is guessed correctly, let GameManager know so it's removed from the letter bank
        // GameManager.Instance.LetterCorrect(letter);

        // If an entire word has been found, let GameManage rknow so it can decrement Words Left
        // GameManager.Instance.WordCorrect("word");
    }
}
