using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessesObject : MonoBehaviour
{
    public void Interact()
    {
        if (!GameManager.Instance.guessesUnlocked)
        {
            Debug.Log("Guesses is locked.");
            return;
        }

        Debug.Log("Final game started.");

        GameManager.Instance.guessesUnlocked = true;
        GameManager.Instance.IncreaseDreamLevel();
    }
}
