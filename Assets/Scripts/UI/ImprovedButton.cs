using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImprovedButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError("Button is null");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!button.interactable) return;
        
        Cursor.SetCursor(MouseCursors.Instance.DefaultMouseTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Texture2D mouseCursor = button.interactable ? MouseCursors.Instance.ClickMouseTexture : MouseCursors.Instance.CrossMouseTexture;
        Cursor.SetCursor(mouseCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(MouseCursors.Instance.DefaultMouseTexture, Vector2.zero, CursorMode.Auto);
    }
}
