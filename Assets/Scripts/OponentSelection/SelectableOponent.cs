using System;
using TMPro;
using UnityEngine;

public class SelectableOponent : MonoBehaviour
{
    public OponentInfoSO OponentInfo => oponentInfo;
    
    [SerializeField] OponentInfoSO oponentInfo;
    [SerializeField] WonAgainstSO wonAgainstSO;
    
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private TextMeshProUGUI _text;
    
    [Space(15)] 
    [SerializeField] Color hoverColor;
    [SerializeField] Color defaultColor;

    [SerializeField] SceneData sceneData;
    [SerializeField] AudioClip selectSound;
    
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    private bool _canDisableColor = true;
    private bool _isSelected;
    
    private void Awake()
    {
        _audioSource = GameObject.FindGameObjectWithTag("SFXSource").GetComponent<AudioSource>();
        canvasObject.SetActive(false);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = defaultColor;
        
      SetText();
    }

    private void OnMouseEnter()
    {
        _spriteRenderer.color = hoverColor;
        canvasObject.SetActive(true);
        Texture2D mouseCursor =
            _isSelected ? MouseCursors.Instance.CrossMouseTexture : MouseCursors.Instance.ClickMouseTexture;
        
        Cursor.SetCursor(mouseCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        if (_isSelected) return;
        
        PlaySound();
        sceneData.SelectedOponent = oponentInfo;
        _canDisableColor = false;
        _isSelected = true;
        
        Cursor.SetCursor(MouseCursors.Instance.CrossMouseTexture, Vector2.zero, CursorMode.Auto);
        OponentSelectionManager.OnSetCurrentOponent?.Invoke(this);
    }

    private void OnMouseExit()
    {
        canvasObject.SetActive(false);
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

    private void SetText()
    {
        string text = wonAgainstSO.Oponents.Contains(oponentInfo) ? "You have won against this oponent" : oponentInfo.HasPlayedAgainstPlayer ? "You have lost against this oponent" : "You have never played against this oponent";
        _text.text = text;
    }

    private void PlaySound()
    {
        if (_audioSource != null )
        {
            _audioSource.PlayOneShot(selectSound);
        }
    }
}
