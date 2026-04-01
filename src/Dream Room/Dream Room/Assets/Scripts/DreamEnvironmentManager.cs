using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamEnvironmentManager : MonoBehaviour
{
    public static DreamEnvironmentManager Instance;

    public Light mainLight;

    [System.Serializable]
    public class DreamStage
    {
        public Color ambientColor;
        public float ambientIntensity = 1f; // 0-1
        public Color fogColor;
        public float fogDensity;

        public Color lightColor;
        public float lightIntensity;

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
        Color targetAmbient = targetStage.ambientColor * targetStage.ambientIntensity;
        Color startLightColor = mainLight.color;
        float startLightIntensity = mainLight.intensity;

        while (time < duration)
        {
            float t = time / duration;

            RenderSettings.ambientLight = Color.Lerp(startAmbient, targetAmbient, t);
            RenderSettings.fogColor = Color.Lerp(startFogColor, targetStage.fogColor, t);
            RenderSettings.fogDensity = Mathf.Lerp(startFogDensity, targetStage.fogDensity, t);
            mainLight.color = Color.Lerp(startLightColor, targetStage.lightColor, t);
            mainLight.intensity = Mathf.Lerp(startLightIntensity, targetStage.lightIntensity, t);

            time += Time.deltaTime;
            yield return null;
        }

        // Snap to final values (ensures accuracy)
        RenderSettings.ambientLight = targetAmbient;
        RenderSettings.fogColor = targetStage.fogColor;
        RenderSettings.fogDensity = targetStage.fogDensity;

        mainLight.color = targetStage.lightColor;
        mainLight.intensity = targetStage.lightIntensity;

        // Apply non-lerped stuff AFTER transition
        HoverableObject.currentHighlightMaterial = targetStage.highlightMaterial;
        PlayerInteraction.Instance.SetHoverSound(targetStage.hoverSound);
    }
}
