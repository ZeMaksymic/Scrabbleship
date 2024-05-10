using System.Collections.Generic;
using UnityEngine;

public class LetterBank : MonoBehaviour
{
    [SerializeField] TileButton tileButtonPrefab;

    public void SetLetters(List<char> letters)
    {
        // Remove any existing tiles
        foreach (Transform child in transform) Destroy(child.gameObject);
        transform.DetachChildren();

        // Create and position a tile for each letter
        for (int i = 0; i < letters.Count; i++)
        {
            TileButton tileButton = Instantiate(tileButtonPrefab, transform);
            tileButton.SetLetter(letters[i]);

            if (i < 4)
            {
                float xPos = 10f + (i * 100f) + (i * 10f);
                float yPos = -10f;
                tileButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            }
            else
            {
                float xPos = 10f + ((i - 4) * 100f) + ((i - 4) * 10f);
                float yPos = -120f;
                tileButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            }
        }
    }
}
