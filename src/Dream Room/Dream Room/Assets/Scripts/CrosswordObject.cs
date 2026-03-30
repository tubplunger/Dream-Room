using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosswordObject : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("Crossword started.");

        GameManager.Instance.crosswordComplete = true;
        GameManager.Instance.IncreaseDreamLevel();
    }
}
