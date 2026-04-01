using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMinigame : MonoBehaviour
{
    public void CompleteGame()
    {
        Debug.Log("Minigame completed");

        GameManager.Instance.IncreaseDreamLevel();

        MinigameManager.Instance.EndMinigame();
    }
}
