using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeObject : MonoBehaviour
{
    public GameObject mazeUI;

    public void Interact()
    {
        if (!GameManager.Instance.mazeUnlocked)
        {
            Debug.Log("Maze is locked!");
            return;
        }

        Debug.Log("Maze Started.");

        MinigameManager.Instance.StartMinigame(mazeUI);
    }

    public void CompleteMaze()
    {
        Debug.Log("Maze finished");

        GameManager.Instance.mazeComplete = true;
        GameManager.Instance.IncreaseDreamLevel();

        MinigameManager.Instance.EndMinigame();

        CheckFinalUnlock();
    }

    void CheckFinalUnlock()
    {
        if (GameManager.Instance.mazeComplete && GameManager.Instance.textBattlerComplete)
        {
            GameManager.Instance.guessesUnlocked = true;
            Debug.Log("Guesses unlocked!");
        }
    }
}
