using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public BoardPiece HeldBoardPiece;
    public BoardSlot HoveredBoardSlot;
    public static Player Instance;
    Vector3 DragOffset;

    void Awake()
    {
        if (Instance)
        {
            return;
        }

        Instance = this;
    }

    RaycastHit[] GetGameObjectAtPosition()
    {
        LayerMask mask = ~0;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.RaycastAll(ray, 999999, mask, QueryTriggerInteraction.Ignore);
    }

    void GrabBoardPiece()
    {
        foreach (RaycastHit hit in GetGameObjectAtPosition())
        {
            BoardPiece DetectedBoardPiece;
            if (hit.transform.gameObject.TryGetComponent<BoardPiece>(out DetectedBoardPiece))
            {
                HeldBoardPiece = DetectedBoardPiece;
                DragOffset = HeldBoardPiece.transform.position - GetMousePosition();

                HeldBoardPiece.GetComponent<SpriteRenderer>().sortingOrder = 10;
            }
        }
    }

    public void OnMouseAction(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            GrabBoardPiece();

            return;
        }

        if (HeldBoardPiece)
        {
            if (HoveredBoardSlot && HoveredBoardSlot.BoardSide == HeldBoardPiece.side)
            {
                HeldBoardPiece.transform.position = HoveredBoardSlot.SlotPosition.position;
            }

            HeldBoardPiece.GetComponent<SpriteRenderer>().sortingOrder = 1;
            HeldBoardPiece = null;
        }
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return mousePosition;
    }

    void DetectSlot()
    {
        BoardSlot DetectedSlot = null;

        foreach (RaycastHit hit in GetGameObjectAtPosition())
        {
            if (hit.transform.gameObject.TryGetComponent(out BoardSlot tempSlot))
            {
                DetectedSlot = hit.transform.gameObject.GetComponent<BoardSlot>();
            }
        }

        HoveredBoardSlot = DetectedSlot;
    }

    void Update()
    {
        DetectSlot();

        if (HeldBoardPiece)
        {
            HeldBoardPiece.transform.position = GetMousePosition() + DragOffset;
        }
    }
}