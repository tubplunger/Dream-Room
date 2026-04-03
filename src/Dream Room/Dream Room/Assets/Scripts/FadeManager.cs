using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;

    public CanvasGroup fadeGroup;

    void Awake()
    {
        instance = this;
    }

    public IEnumerator FadeOut(float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            fadeGroup.alpha = Mathf.Lerp(0f, 1f, time / duration);

            time += Time.unscaledDeltaTime;
            yield return null;
        }

        fadeGroup.alpha = 1f;
    }
}
