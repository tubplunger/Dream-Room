using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessesObject : MonoBehaviour
{
    public GameObject guessesUI;

    public void Interact()
    {
        if (!GameManager.Instance.guessesUnlocked)
        {
            Debug.Log("Guesses is locked.");
            return;
        }

        Debug.Log("Final game started.");

        GameManager.Instance.guessesUnlocked = true;
        MinigameManager.Instance.StartMinigame(guessesUI);
    }

    public void CompleteGuesses()
    {
        GameManager.Instance.IncreaseDreamLevel();
        MinigameManager.Instance.EndMinigame();

        StartCoroutine(EndGameSequence());
    }

    private IEnumerator EndGameSequence()
    {
        yield return new WaitForSecondsRealtime(6f);

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
