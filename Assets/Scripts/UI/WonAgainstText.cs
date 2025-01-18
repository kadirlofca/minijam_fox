using System;
using TMPro;
using UnityEngine;

public class WonAgainstText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI wonAgainstText;
    [SerializeField] WonAgainstSO wonAgainstSO;

    private void Awake()
    {
        wonAgainstText.text = "You have won against: ";

        int opponentCount = wonAgainstSO.Oponents.Count;
            
        for (int i = 0; i < opponentCount; i++)
        {
            wonAgainstText.text += wonAgainstSO.Oponents[i].OponentName;

            if (i == opponentCount - 2)
            {
                wonAgainstText.text += " and ";
            }
            else if (i < opponentCount - 2) 
            {
                wonAgainstText.text += ", ";
            }
        }
    }
}
