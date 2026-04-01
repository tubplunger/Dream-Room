using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool crosswordComplete = false;
    public bool colorSolved = false;
    public bool mazeComplete = false;
    public bool textBattlerComplete = false;
    public bool guessesUnlocked = false;

    public int dreamLevel = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        DreamEnvironmentManager.Instance.ApplyStage(dreamLevel);
    }

    public void IncreaseDreamLevel()
    {
        dreamLevel++;
        Debug.Log("Dream level increased to: " + dreamLevel);

        DreamEnvironmentManager.Instance.ApplyStage(dreamLevel);
    }
}
