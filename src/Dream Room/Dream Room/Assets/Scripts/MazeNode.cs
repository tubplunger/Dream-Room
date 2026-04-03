using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeNode : MonoBehaviour
{
    public string nodeColor;
    private MazeManager manager;

    void Start()
    {
        manager = FindObjectOfType<MazeManager>();
    }

    public void OnClick()
    {
        manager.SelectNode(this);
    }
}
