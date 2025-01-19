using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OponentInfoSO", menuName = "Scriptable Objects/OponentInfoSO")]
public class OponentInfoSO : ScriptableObject
{
    public string ID;
    [Space(15)]
    public string OponentName;
    
    public Sprite OponentSprite;
    
    [Space(15)]
    [Header("Toss coin settings")]
    
    [Range(0,100)]
    public float PlayerWinChance = 50;
    [Range(0,100)]
    public float OponentWinChance = 50;
    
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(ID)) 
        {
            ID = Guid.NewGuid().ToString();
        }
    }
    
}
