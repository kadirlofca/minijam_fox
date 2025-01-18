using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardSlot : MonoBehaviour
{
    [SerializeField]
    public Transform SlotPosition;

    void OnMouseEnter()
    {
        Player.Instance.HoveredBoardSlot = this;

        GetComponent<SpriteHighlight>().HighlightActive = true;
    }

    void OnMouseExit()
    {
        Player.Instance.HoveredBoardSlot = null;
        GetComponent<SpriteHighlight>().HighlightActive = false;
    }
}
