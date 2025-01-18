using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    public BoardPiece CurrentlyHeldBoardPiece;

    public BoardSlot CurrentlyHoveredBoardSlot;

    public static Player Instance;

    void Awake()
    {
        if (Instance)
        {
            return;
        }

        Instance = this;
    }

    public void OnMouseAction(InputAction.CallbackContext context)
    {
        // Debug.Log(context.ReadValueAsButton());
        Debug.Log(CurrentlyHoveredBoardSlot);

    }
}
