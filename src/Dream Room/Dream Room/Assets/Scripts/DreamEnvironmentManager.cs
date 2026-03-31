using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamEnvironmentManager : MonoBehaviour
{
    public static DreamEnvironmentManager Instance;

    [System.Serializable]
    public class DreamStage
    {
        public Color ambientColor;
        public Color fogColor;
        public float fogDensity;

        public Material highlightMaterial;
        public AudioClip hoverSound;
    }

    public DreamStage[] stages;

    void Awake()
    {
        Instance = this;
    }

    public void ApplyStage(int level)
    {
        if (level < 0 || level >= stages.Length) return;

        StopAllCoroutines();
        StartCoroutine(TransitionToStage(stages[level]));

        Debug.Log("Transitioning to dream stage: " + level);
    }

    IEnumerator TransitionToStage(DreamStage targetStage)
    {
        float duration = 2f; // how long the transition takes
        float time = 0f;

        Color startAmbient = RenderSettings.ambientLight;
        Color startFogColor = RenderSettings.fogColor;
        float startFogDensity = RenderSettings.fogDensity;

        while (time < duration)
        {
            float t = time / duration;

            RenderSettings.ambientLight = Color.Lerp(startAmbient, targetStage.ambientColor, t);
            RenderSettings.fogColor = Color.Lerp(startFogColor, targetStage.fogColor, t);
            RenderSettings.fogDensity = Mathf.Lerp(startFogDensity, targetStage.fogDensity, t);

            time += Time.deltaTime;
            yield return null;
        }

        // Snap to final values (ensures accuracy)
        RenderSettings.ambientLight = targetStage.ambientColor;
        RenderSettings.fogColor = targetStage.fogColor;
        RenderSettings.fogDensity = targetStage.fogDensity;

        // Apply non-lerped stuff AFTER transition
        HoverableObject.currentHighlightMaterial = targetStage.highlightMaterial;
        PlayerInteraction.Instance.SetHoverSound(targetStage.hoverSound);
    }
}
