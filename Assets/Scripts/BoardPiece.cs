using System.Collections.Specialized;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    private Vector3 DragOffset;

    void OnMouseDown()
    {
        DragOffset = transform.position - GetMousePosition();
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePosition() + DragOffset;
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return mousePosition;
    }
}
