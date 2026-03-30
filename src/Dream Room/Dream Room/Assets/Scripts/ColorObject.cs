using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    public Color correctColor = Color.red;
    private bool solved = false;

    public void Interact()
    {
        Debug.Log("Color puzzle opened.");

        // simulate color
        CheckColor(Color.red); // replace with actual input
    }

    void CheckColor(Color chosenColor)
    {
        if (chosenColor == correctColor && !solved)
        {
            solved = true;

            Debug.Log("Correct color chosen!");

            GameManager.Instance.colorSolved = true;
            GameManager.Instance.IncreaseDreamLevel();

            UnlockNextGames();
            ChangeRoomColor(chosenColor);
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
