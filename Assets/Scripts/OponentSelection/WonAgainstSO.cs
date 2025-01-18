using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WonAgainstSO", menuName = "Scriptable Objects/WonAgainstSO")]
public class WonAgainstSO : ScriptableObject
{
    public List<OponentInfoSO> Oponents;
    
    public void AddOponent(OponentInfoSO oponentInfoSO)
    {
        Oponents.Add(oponentInfoSO);
    }
}
