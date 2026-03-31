using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverableObject : MonoBehaviour
{
    public static Material currentHighlightMaterial;

    private Material originalMaterial;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;
    }

    public void OnHoverEnter()
    {
        if (currentHighlightMaterial != null)
        {
            rend.material = currentHighlightMaterial;
        }
    }

    public void OnHoverExit()
    {
        rend.material = originalMaterial;
    }
}
