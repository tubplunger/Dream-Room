using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrosswordManager : MonoBehaviour
{
    [System.Serializable]
    public class Clue 
    {
        public string correctAnswer;
        public TMP_InputField inputField;
    }

    public Clue[] clues;

    public CrosswordObject crosswordObject;

    public void CheckAnswers()
    {
        bool allCorrect = true;

        foreach (Clue clue in clues)
        {
            string playerAnswer = clue.inputField.text.Trim().ToLower();
            string correct = clue.correctAnswer.ToLower();

            if (playerAnswer != correct)
            {
                allCorrect = false;
                clue.inputField.image.color = Color.red;
            }
            else
            {
                clue.inputField.image.color = Color.green;
            }
        }

        if (allCorrect)
        {
            Debug.Log("All answers correct!");
            crosswordObject.CompleteCrossword();
        }
        else
        {
            Debug.Log("Some answers are incorrect.");
        }
    }
}
