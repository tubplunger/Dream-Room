using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuessesManager : MonoBehaviour
{
    public TMP_Text responseText;
    public TMP_InputField inputField;

    public GuessesObject guessesObject;

    private int stage = 0;
    private bool finished = false;

    void OnEnable()
    {
        responseText.text = "Guess what I am.";
        inputField.text = "";
        finished = false;
        stage = 0;
    }

    public void SubmitGuess()
    {
        if (finished) return;

        string guess = inputField.text.ToLower().Trim();
        inputField.text = "";

        StartCoroutine(ProcessGuess(guess));
    }

    IEnumerator ProcessGuess(string guess)
    {
        yield return ShowText("> " + guess);

        if (guess.Contains("monster"))
        {
            yield return ShowText("No.");
        }
        else if (guess.Contains("dream"))
        {
            yield return ShowText("Not anymore.");
        }
        else if (guess.Contains("nightmare"))
        {
            yield return ShowText("Closer.");
            stage++;
        }
        else if (guess.Contains("human"))
        {
            yield return ShowText("I was.");
            stage++;
        }
        else if (guess.Contains("brother") || guess.Contains("sister") || guess.Contains("sibling"))
        {
            yield return FinalReveal();
            yield break;
        }
        else
        {
            yield return ShowText("You keep saying that...");
        }

        CheckProgress();
    }

    void CheckProgress()
    {
        if (stage >= 3)
        {
            StartCoroutine(ForceReveal());
        }
    }

    IEnumerator ForceReveal()
    {
        finished = true;

        yield return ShowText("You already know.");
        yield return ShowText("You just won't say it.");

        yield return FinalReveal();
    }

    IEnumerator FinalReveal()
    {
        finished = true;

        yield return ShowText("It's me.");
        yield return ShowText("You left me here.");

        yield return StartCoroutine(GlitchRoutine("Why didn't you come back?", 2f, 0.8f));

        yield return new WaitForSecondsRealtime(2f);

        guessesObject.CompleteGuesses();
    }

    IEnumerator ShowText(string text)
    {
        responseText.text = "";

        foreach (char c in text)
        {
            responseText.text += c;
            yield return new WaitForSecondsRealtime(0.03f);
        }

        yield return new WaitForSecondsRealtime(1f);
    }

    string GlitchText(string original, float intensity)
    {
        string chars = "@#$%^&*()_+-=<>?/{}[]";

        char[] result = original.ToCharArray();

        for (int i = 0; i < result.Length; i++)
        {
            if (Random.value < intensity)
            {
                result[i] = chars[Random.Range(0, chars.Length)];
            }
        }

        return new string(result);
    }

    IEnumerator GlitchRoutine(string baseText, float duration, float intensity)
    {
        float time = 0f;

        Color originalColor = responseText.color;
        Vector3 originalPos = responseText.rectTransform.localPosition;

        while (time < duration)
        {
            responseText.text = GlitchText(baseText, intensity);

            responseText.color = Color.Lerp(originalColor, Color.red, intensity);

            responseText.rectTransform.localPosition = originalPos + (Vector3)(Random.insideUnitCircle * (2f * intensity));

            yield return new WaitForSecondsRealtime(0.05f);

            time += 0.05f;
        }

        responseText.text = baseText;
        responseText.color = originalColor;
        responseText.rectTransform.localPosition = originalPos;
    }
}
