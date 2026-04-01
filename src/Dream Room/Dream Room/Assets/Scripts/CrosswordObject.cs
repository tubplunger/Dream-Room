using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosswordObject : MonoBehaviour
{
    public GameObject crosswordUI;

    public void Interact()
    {
        Debug.Log("Crossword started.");

        MinigameManager.Instance.StartMinigame(crosswordUI);
    }

    public void CompleteCrossword()
    {
        Debug.Log("Crossword completed.");

        GameManager.Instance.crosswordComplete = true;
        GameManager.Instance.IncreaseDreamLevel();

        MinigameManager.Instance.EndMinigame();
    }
}
