using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;

    [Header("Interaction")]
    public float interactDistance = 5f;
    public Camera playerCamera;

    private HoverableObject currentHover;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hoverSound;

    void Awake()
    {
        Instance = this;
    }

    public void SetHoverSound(AudioClip newSound)
    {
        hoverSound = newSound;
    }

    void Update()
    {
        if (MinigameManager.Instance != null &&
        MinigameManager.Instance.isMinigameActive)
        {
            ClearHover();
            return;
        }

        HandleHover();

        if (Input.GetMouseButtonDown(0))
        {
            TryClick();
        }
    }

    public void ClearHover()
    {
        if (currentHover != null)
        {
            currentHover.OnHoverExit();
            currentHover = null;
        }
    }

    void HandleHover()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        HoverableObject newHover = null;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            newHover = hit.collider.GetComponent<HoverableObject>();
        }

        // If looking at a new object
        if (newHover != currentHover)
        {
            // Remove highlight from old one
            if (currentHover != null)
            {
                currentHover.OnHoverExit();
            }

            // Add highlight to new one
            if (newHover != null)
            {
                newHover.OnHoverEnter();

                // Play hover sound
                if (audioSource != null && hoverSound != null)
                {
                    // If it's a static sound, play a short burst
                    if (hoverSound.name.Contains("Static"))
                    {
                        StartCoroutine(PlayShortSound(hoverSound, 0.1f));
                    }
                    else
                    {
                        audioSource.PlayOneShot(hoverSound);
                    }
                }
            }

            currentHover = newHover;
        }
    }

    void TryClick()
    {
        if (MinigameManager.Instance != null &&
        MinigameManager.Instance.isMinigameActive)
        {
            return;
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            hit.collider.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator PlayShortSound(AudioClip clip, float duration)
    {
        if (audioSource == null || clip == null) yield break;

        audioSource.clip = clip;
        audioSource.Play();

        yield return new WaitForSeconds(duration);

        audioSource.Stop();
    }
}
