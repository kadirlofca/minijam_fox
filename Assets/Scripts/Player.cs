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

    public void OnMouseAction(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
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

            return;
        }

        HeldBoardPiece.GetComponent<SpriteRenderer>().sortingOrder = 1;
        HeldBoardPiece = null;
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return mousePosition;
    }

    void Update()
    {
        if (HeldBoardPiece)
        {
            HeldBoardPiece.transform.position = GetMousePosition() + DragOffset;
        }
    }
}