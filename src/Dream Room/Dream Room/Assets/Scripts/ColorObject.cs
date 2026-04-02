using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorObject : MonoBehaviour
{
    public GameObject colorUI;

    [Header("UI")]
    public TMP_Text questionText;

    [Header("Questions")]
    private int currentQuestion = 0;

    private bool firstAnswered = false;
    private bool secondAnswered = false;

    void Start()
    {
        UpdateQuestion();
    }

    public void Interact()
    {
        Debug.Log("Color puzzle opened.");

        UpdateQuestion();
        MinigameManager.Instance.StartMinigame(colorUI);
    }

    void UpdateQuestion()
    {
        if (!firstAnswered)
        {
            currentQuestion = 0;
            questionText.text = "What was the color of the sky that day?";
        }
        else if (!secondAnswered)
        {
            currentQuestion = 1;
            questionText.text = "What was the color of my blood when you left?";
        }
        else
        {
            questionText.text = "There is nothing left to remember.";
        }
    }

    // BUTTON FUNCTIONS
    public void ChooseRed() => CheckColor(Color.red);
    public void ChooseBlue() => CheckColor(Color.blue);
    public void ChooseGreen() => CheckColor(Color.green);

    void CheckColor(Color chosenColor)
    {
        Debug.Log("Color chosen: " + chosenColor);

        if (currentQuestion == 0 && chosenColor == Color.blue)
        {
            Debug.Log("Correct (Sky)");

            firstAnswered = true;

            // Unlock Maze
            GameManager.Instance.mazeUnlocked = true;
            Debug.Log("Maze unlocked");

            GameManager.Instance.IncreaseDreamLevel();

            MinigameManager.Instance.EndMinigame();
        }
        else if (currentQuestion == 1 && chosenColor == Color.red)
        {
            Debug.Log("Correct (Blood)");

            secondAnswered = true;

            // Unlock Text Battler
            GameManager.Instance.textBattlerUnlocked = true;
            Debug.Log("Text Battler unlocked");

            GameManager.Instance.IncreaseDreamLevel();

            MinigameManager.Instance.EndMinigame();
        }
        else
        {
            Debug.Log("Wrong answer.");
        }

        CheckCompletion();
    }

    void CheckCompletion()
    {
        if (firstAnswered && secondAnswered)
        {
            Debug.Log("Color puzzle fully completed.");

            GameManager.Instance.colorSolved = true;
        }
    }
}
