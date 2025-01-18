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
        Player.Instance.CurrentlyHoveredBoardSlot = this;
    }

    void OnMouseExit()
    {
        Player.Instance.CurrentlyHoveredBoardSlot = null;
    }
}
