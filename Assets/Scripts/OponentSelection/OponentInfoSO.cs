using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OponentInfoSO", menuName = "Scriptable Objects/OponentInfoSO")]
public class OponentInfoSO : ScriptableObject
{
    public string ID;
    [Space(15)]
    public string OponentName;
    
    public Sprite OponentSprite;
    
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(ID)) 
        {
            ID = Guid.NewGuid().ToString();
        }
    }
    
}
