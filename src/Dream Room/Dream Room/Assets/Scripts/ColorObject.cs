using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    public Color correctColor = Color.red;
    private bool solved = false;

    public GameObject colorUI;

    public void Interact()
    {
        Debug.Log("Color puzzle opened.");

        if (colorUI == null)
        {
            Debug.LogError("Color UI is NOT assigned!");
            return;
        }

        MinigameManager.Instance.StartMinigame(colorUI);
    }

    public void ChooseRed()
    {
        CheckColor(Color.red);
    }

    public void CheckColor(Color chosenColor)
    {
        if (chosenColor == correctColor && !solved)
        {
            solved = true;

            Debug.Log("Correct color chosen!");

            GameManager.Instance.colorSolved = true;
            GameManager.Instance.IncreaseDreamLevel();

            MinigameManager.Instance.EndMinigame();

            UnlockNextGames();
            ChangeRoomColor(chosenColor);
        }
        else
        {
            Debug.Log("Wrong color!");
        }
    }

    void UnlockNextGames()
    {
        Debug.Log("Maze or Text Battler unlocked.");
    }

    void ChangeRoomColor(Color newColor)
    {
        RenderSettings.ambientLight = newColor;
        Debug.Log("Room color changed.");
    }
}
