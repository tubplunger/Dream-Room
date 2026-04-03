using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBattleManager : MonoBehaviour
{
    public TMP_Text battleText;

    public TextBattlerObject textBattlerObject;

    private int step = 0;

    private bool waitingForChoice = false;

    void OnEnable()
    {
        step = 0;
        StartCoroutine(BattleSequence());
    }

    IEnumerator BattleSequence()
    {
        yield return ShowText("Your Nightmare has finally appeared!");
        yield return ShowText("Your ally has gone to get help.");

        waitingForChoice = true;
    }

    public void ChooseAction(string action)
    {
        if (!waitingForChoice)
        {
            return;
        }

        waitingForChoice = false;

        StartCoroutine(ResolveTurn(action));
    }

    IEnumerator ResolveTurn(string action)
    {
        yield return ShowText("You chose to " + action + "...");

        if (step == 0)
        {
            yield return ShowText("The Nightmare doesn't react.");
        }
        else if (step == 1)
        {
            yield return ShowText("Your actions feel... meaningless.");
        }
        else if (step == 2)
        {
            yield return ShowText("Something is wrong.");
        }

        step++;

        if (step >= 3)
        {
            StartCoroutine(EndSequence());
        }
        else
        {
            waitingForChoice = true;
        }
    }

    IEnumerator EndSequence()
    {
        yield return ShowText("The Nightmare begins to destort...");
        yield return ShowText("You cannot win.");

        string finalLine = "Why didn't you come back?";

        yield return ShowText(finalLine);

        for (float i = 0.3f; i <= 1f; i += 0.3f)
        {
            yield return StartCoroutine(GlitchRoutine(finalLine, 2f, i));
        }

        battleText.text = RandomGlitchText();

        yield return new WaitForSecondsRealtime(1f);

        textBattlerObject.CompleteText();
    }

    IEnumerator ShowText(string text)
    {
        battleText.text = "";

        foreach(char c in text)
        {
            battleText.text += c;
            yield return new WaitForSecondsRealtime(0.03f);
        }

        yield return new WaitForSecondsRealtime(2f);
    }

    string RandomGlitchText()
    {
        string chars = "@#$%^&*()_+-=<>?/{}[]";
        string result = "";

        for (int i = 0; i < 20; i++)
        {
            result += chars[Random.Range(0, chars.Length)];
        }

        return result;
    }

    string GlitchText(string original, float intensity)
    {
        string chars = "@#$%^&*()_+-=<>?/{}[]";

        char[] result = original.ToCharArray();

        for (int i = 0; i < result.Length;  i++)
        {
            if (Random.value < intensity)
            {
                result[i] = chars[Random.Range(0, chars.Length)];
            }
        }

        return new string (result);
    }

    IEnumerator GlitchRoutine(string baseText, float duration, float intensity)
    {
        float time = 0f;

        Color originalColor = battleText.color;
        Vector3 originalPos = battleText.rectTransform.localPosition;

        while (time < duration)
        {
            battleText.text = GlitchText(baseText, intensity);

            battleText.color = Color.Lerp(originalColor, Color.red, intensity);

            battleText.rectTransform.localPosition = originalPos + (Vector3)(Random.insideUnitCircle * (2f * intensity));

            yield return new WaitForSecondsRealtime(0.05f);

            time += 0.05f;
        }

        battleText.text = baseText;
        battleText.color = originalColor;
        battleText.rectTransform.localPosition = originalPos;
    }
}
