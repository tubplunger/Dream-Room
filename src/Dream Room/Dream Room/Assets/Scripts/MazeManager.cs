using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    private MazeNode firstSelected;

    private List<string> completedColors = new List<string>();

    public MazeObject mazeObject;

    public void SelectNode(MazeNode node)
    {
        if (firstSelected == null)
        {
            firstSelected = node;
            Debug.Log("Selected first: " + node.nodeColor);
            return;
        }

        if (node == firstSelected )
        {
            return;
        }

        if (node.nodeColor == firstSelected.nodeColor && !completedColors.Contains(node.nodeColor))
        {
            Debug.Log("Connected: " + node.nodeColor);

            completedColors.Add(node.nodeColor);

            // Draw line later
        }
        else
        {
            Debug.Log("Invalid connection");
        }

        firstSelected = null;

        CheckWin();
    }

    void CheckWin()
    {
        if (completedColors.Count >= 3)
        {
            Debug.Log("Maze Puzzle complete!");
            mazeObject.CompleteMaze();
        }
    }
}
