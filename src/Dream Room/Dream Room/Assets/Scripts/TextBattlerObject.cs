using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBattlerObject : MonoBehaviour
{
    public GameObject textUI;

    public void Interact()
    {
        if (!GameManager.Instance.colorSolved)
        {
            Debug.Log("Text Battler is locked.");
            return;
        }

        Debug.Log("Text Battler started.");

        MinigameManager.Instance.StartMinigame(textUI);
    }

    public void CompleteText()
    {
        Debug.Log("Text Battler finished");

        GameManager.Instance.textBattlerComplete = true;
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
