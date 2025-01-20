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

    public Sprite coinPlus;
    public Sprite coinMinus;

    public Transform coinLandTransform;

    public bool beingTossed = false;

    public AudioSource audio;


    IEnumerator WaitAndBehave()
    {
        yield return new WaitForSeconds(0.2f);


        if (progress == 0)
        {
            audio.Play();

            beingTossed = true;
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

            coinFive.transform.position = coinLandTransform.position;
            coinFive.transform.localScale = coinLandTransform.localScale;

            float winChance = BoardState.Instance.turn.side ? Opponent.Instance.oponentInfoSO.OponentWinChance : Opponent.Instance.oponentInfoSO.PlayerWinChance;
            CoinTossState coinResult = Random.Range(0, 100) < winChance ? CoinTossState.Forward : CoinTossState.Back;
            BoardState.Instance.turn.coinTossResult = coinResult;

            coinFive.GetComponent<SpriteRenderer>().sprite = coinResult == CoinTossState.Forward ? coinPlus : coinMinus;
        }
        else if (progress == 5 && BoardState.Instance.turn.side)
        {
            Opponent.Instance.MovePiece(BoardState.Instance.turn.coinTossResult == CoinTossState.Forward);
            BoardState.Instance.EndTurn();
        }
        else
        {
            beingTossed = false;
            yield return 0;
        }

        progress++;

        StartCoroutine(WaitAndBehave());
    }

    public void Toss()
    {
        StartCoroutine(WaitAndBehave());
    }

    public void Reset()
    {
        StopAllCoroutines();
        progress = 0;
    }
}
