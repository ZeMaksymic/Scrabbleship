using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject instructionsPanel;

    public void ShowInstructions(bool show)
    {
        instructionsPanel.SetActive(show);
    }
}
