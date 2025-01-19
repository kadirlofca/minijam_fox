using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OponentInfoSO", menuName = "Scriptable Objects/OponentInfoSO")]
public class OponentInfoSO : ScriptableObject
{
    public string ID;
    public bool HasPlayedAgainstPlayer;
    
    [Space(15)]
    public string OponentName;
    public float BehaviorDuration = 10;
    [Space(10)]
    public Sprite Idle;
    public Sprite Distracted;
    public Sprite Suspicious;
    public Sprite Confrontational;
    
    
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
