using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHighlight : MonoBehaviour
{
    public bool HighlightActive = false;
    private Material material;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (HighlightActive)
        {
            material.SetFloat("_active", 1);

            return;
        }

        material.SetFloat("_active", 0);
    }
}
