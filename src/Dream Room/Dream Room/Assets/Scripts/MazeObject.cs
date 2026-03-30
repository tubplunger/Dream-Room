using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeObject : MonoBehaviour
{
    public void Interact()
    {
        if (!GameManager.Instance.colorSolved)
        {
            Debug.Log("Maze is locked!");
            return;
        }

        Debug.Log("Maze Started.");

        GameManager.Instance.mazeComplete = true;
        GameManager.Instance.IncreaseDreamLevel();

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
