using UnityEngine;

public class MouseCursors : MonoBehaviour
{
    public static MouseCursors Instance { get; private set; }
    
    public Texture2D ClickMouseTexture => clickMouseTexture;
    public Texture2D DefaultMouseTexture => defaultMouseTexture;
    public Texture2D CrossMouseTexture => crossMouseTexture;
    
    [SerializeField] private Texture2D defaultMouseTexture;
    [SerializeField] private Texture2D clickMouseTexture;
    [SerializeField] private Texture2D crossMouseTexture;
    
    private void Awake()
    {
        Instance = this;
    }
    
    
}
