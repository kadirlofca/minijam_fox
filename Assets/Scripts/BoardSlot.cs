using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardSlot : MonoBehaviour
{
    [SerializeField]
    public Transform SlotPosition;
    public Sprite HighlightSprite;
    public Sprite DefaultSprite;
    public int ProgressValue = 0;
    public bool BoardSide = false;

    public void Highlight()
    {
        GetComponent<SpriteRenderer>().sprite = HighlightSprite;
    }

    public void UnHighlight()
    {
        GetComponent<SpriteRenderer>().sprite = DefaultSprite;

    }

    void Awake()
    {
        DefaultSprite = GetComponent<SpriteRenderer>().sprite;
    }
}
