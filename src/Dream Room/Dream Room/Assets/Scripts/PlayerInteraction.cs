using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 5f;
    public Camera playerCamera;

    private HoverableObject currentHover;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hoverSound;

    void Update()
    {
        HandleHover();

        if (Input.GetMouseButtonDown(0))
        {
            TryClick();
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

        // looking at a new object
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

                if (audioSource != null && hoverSound != null)
                {
                    audioSource.PlayOneShot(hoverSound);
                } 
            }

            currentHover = newHover;
        }
    }

    void TryClick()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            hit.collider.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
        }
    }
}
