using UnityEngine;
using System.Collections;

public enum OpponentBehavior
{
    Idle,
    Distracted,
    Suspicious,
    Confrontational
}

public class Opponent : MonoBehaviour
{
    public float SusLevel = 0;

    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(5);
        print("WaitAndPrint " + Time.time);
    }

    IEnumerator Start()
    {
        print("Starting " + Time.time);

        // Start function WaitAndPrint as a coroutine
        yield return StartCoroutine("WaitAndPrint");
        print("Done " + Time.time);
    }
}
