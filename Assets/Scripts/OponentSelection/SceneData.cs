using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scriptable Objects/SceneData")]
public class SceneData : ScriptableObject
{
    private int selectedOponent;
    public int SelectedOponent { get => selectedOponent; set => selectedOponent = value; }
    
    
}
