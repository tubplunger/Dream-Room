using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBattlerObject : MonoBehaviour
{
    public void Interact()
    {
        if (!GameManager.Instance.colorSolved)
        {
            Debug.Log("Text Battler is locked.");
            return;
        }

        Debug.Log("Text Battler started.");

        GameManager.Instance.textBattlerComplete = true;
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
