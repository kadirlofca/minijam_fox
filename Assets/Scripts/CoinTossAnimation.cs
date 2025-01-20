using UnityEngine;
using System.Collections;

public class CoinTossAnimation : MonoBehaviour
{
    int progress = 0;

    public GameObject coinOne;
    public GameObject coinTwo;
    public GameObject coinThree;
    public GameObject coinFour;
    public GameObject coinFive;

    IEnumerator WaitAndBehave()
    {
        yield return new WaitForSeconds(0.2f);

        if (progress == 0)
        {
            coinOne.SetActive(true);
            coinTwo.SetActive(false);
            coinThree.SetActive(false);
            coinFour.SetActive(false);
            coinFive.SetActive(false);
        }
        else if (progress == 1)
        {
            coinOne.SetActive(false);
            coinTwo.SetActive(true);
            coinThree.SetActive(false);
            coinFour.SetActive(false);
            coinFive.SetActive(false);
        }
        else if (progress == 2)
        {
            coinOne.SetActive(false);
            coinTwo.SetActive(false);
            coinThree.SetActive(true);
            coinFour.SetActive(false);
            coinFive.SetActive(false);
        }
        else if (progress == 3)
        {
            coinOne.SetActive(false);
            coinTwo.SetActive(false);
            coinThree.SetActive(false);
            coinFour.SetActive(true);
            coinFive.SetActive(false);
        }
        else if (progress == 4)
        {
            coinOne.SetActive(false);
            coinTwo.SetActive(false);
            coinThree.SetActive(false);
            coinFour.SetActive(false);
            coinFive.SetActive(true);
        }

        if (progress >= 4)
        {
            yield return 0;
        }

        progress++;

        StartCoroutine(WaitAndBehave());
    }

    void Start()
    {
        StartCoroutine(WaitAndBehave());
    }
}
