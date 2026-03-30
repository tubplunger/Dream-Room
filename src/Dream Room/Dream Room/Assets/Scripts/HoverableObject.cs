using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverableObject : MonoBehaviour
{
    private Material originalMaterial;
    public Material highlightMaterial;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;
    }

    public void OnHoverEnter()
    {
        if (highlightMaterial != null)
        {
            rend.material = highlightMaterial;
        }
    }

    public void OnHoverExit()
    {
        rend.material = originalMaterial;
    }
}
