using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardSlot : MonoBehaviour
{
    [SerializeField]
    public Transform SlotPosition;
    public int ProgressValue = 0;
    public bool BoardSide = false;
}
