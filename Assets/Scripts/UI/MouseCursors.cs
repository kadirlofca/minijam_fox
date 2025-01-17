using UnityEngine;

public class MouseCursors : MonoBehaviour
{
    public static MouseCursors Instance { get; private set; }
    public Texture2D ClickMouseTexture => clickMouseTexture;
    public Texture2D DefaultMouseTexture => defaultMouseTexture;
    
    [SerializeField] private Texture2D defaultMouseTexture;
    [SerializeField] private Texture2D clickMouseTexture;
    private void Awake()
    {
        Instance = this;
    }
    
    
}
