using System;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _hoverColor;
    
    [SerializeField]
    public bool side = false;

    public BoardSlot currentSlot = null;

    void UpdateBoardState()
    {
        if (side)
        {
            BoardState.Instance.OpponentBoardProgress = currentSlot.ProgressValue;
        }
        else
        {
            BoardState.Instance.AllyBoardProgress = currentSlot.ProgressValue;
        }
    }

    public void OnPlaced()
    {
        float scale = side ? (currentSlot.ProgressValue * 0.1f) + 0.6f : (currentSlot.ProgressValue * -0.1f) + 1f;
        transform.localScale = new Vector3(scale, scale, scale);

        UpdateBoardState();
    }

    private void OnMouseEnter()
    {
        _spriteRenderer.color = _hoverColor;
    }
    
    private void OnMouseExit()
    {
        _spriteRenderer.color = _normalColor;
    }
}
