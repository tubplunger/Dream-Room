using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 5f;
    public Camera playerCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

                if (interactable != null)
                {
                    interactable.Interact();
                }   
            }
        }
    }
}
