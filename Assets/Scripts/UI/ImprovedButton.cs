using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImprovedButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioClip _clickSound;
    
    private AudioSource _audioSource;
    private Button _button;
   
    private void Awake()
    {
        _button = GetComponent<Button>();
        _audioSource = GameObject.FindGameObjectWithTag("SFXSource").GetComponent<AudioSource>();
        
        if (_button == null)
        {
            Debug.LogError("Button is null");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_button.interactable) return;
        
        PlayClickSound();
        Cursor.SetCursor(MouseCursors.Instance.DefaultMouseTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Texture2D mouseCursor = _button.interactable ? MouseCursors.Instance.ClickMouseTexture : MouseCursors.Instance.CrossMouseTexture;
        Cursor.SetCursor(mouseCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(MouseCursors.Instance.DefaultMouseTexture, Vector2.zero, CursorMode.Auto);
    }
    
    private void PlayClickSound()
    {
        if (_clickSound != null)
        {
            _audioSource.PlayOneShot(_clickSound);
        }
    }
}
