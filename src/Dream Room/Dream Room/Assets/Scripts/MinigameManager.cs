using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    public GameObject currentMinigameUI;

    public bool isMinigameActive;

    void Awake()
    {
        Instance = this;
    }

    public void StartMinigame(GameObject minigameUI)
    {
        if (isMinigameActive)
        {
            Debug.LogWarning("A minigame is already active!");
            return;
        }

        isMinigameActive = true;
        currentMinigameUI = minigameUI;

        currentMinigameUI.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EndMinigame()
    {
        Debug.Log("Ending minigame");

        if (currentMinigameUI != null)
        {
            currentMinigameUI.SetActive(false);
            currentMinigameUI = null;
        }

        isMinigameActive = false;

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
