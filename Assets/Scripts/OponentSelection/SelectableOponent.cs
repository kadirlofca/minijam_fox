using System;
using UnityEngine;

public class SelectableOponent : MonoBehaviour
{
    [SerializeField] OponentInfoSO oponentInfo;  
    [Space(10)] 
    [SerializeField] Color hoverColor;
    [SerializeField] Color defaultColor;

    [SerializeField] SceneData sceneData;
 
    private SpriteRenderer _spriteRenderer;

    private bool _canDisableColor = true;
    private bool _isSelected;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = defaultColor;
    }

    private void OnMouseEnter()
    {
        _spriteRenderer.color = hoverColor;

        Texture2D mouseCursor =
            _isSelected ? MouseCursors.Instance.CrossMouseTexture : MouseCursors.Instance.ClickMouseTexture;
        
        Cursor.SetCursor(mouseCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        if (_isSelected) return;
        
        sceneData.SelectedOponent = oponentInfo;
        _canDisableColor = false;
        _isSelected = true;
        
        Cursor.SetCursor(MouseCursors.Instance.CrossMouseTexture, Vector2.zero, CursorMode.Auto);
        OponentSelectionManager.OnSetCurrentOponent?.Invoke(this);
    }

    private void OnMouseExit()
    {
        if (_canDisableColor)
            _spriteRenderer.color = defaultColor;
        
        Cursor.SetCursor(MouseCursors.Instance.DefaultMouseTexture, Vector2.zero, CursorMode.Auto);
    }

    public void DisableSelection()
    {
        _isSelected = false;
        _canDisableColor = true;
        _spriteRenderer.color = defaultColor;
    }

}
