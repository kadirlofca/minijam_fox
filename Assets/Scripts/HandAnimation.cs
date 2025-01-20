using UnityEngine;
using System.Collections;

public class HandAnimation : MonoBehaviour
{
    int progress = 0;

    [SerializeField]
    public SpriteRenderer handrenderer;
    public Sprite handDefault;
    public Sprite handCoin;
    public Sprite handToss;


    IEnumerator WaitAndBehave()
    {
        yield return new WaitForSeconds(0.2f);

        if (progress == 0)
        {
            handrenderer.sprite = handDefault;
        }
        else if (progress == 1)
        {
            handrenderer.sprite = handCoin;
        }
        else if (progress == 2)
        {
            handrenderer.sprite = handToss;
            Player.Instance.tossAnim.Toss();
        }
        else if (progress == 3)
        {
            handrenderer.sprite = handDefault;

        }
        else
        {
            yield return 0;
        }

        progress++;

        StartCoroutine(WaitAndBehave());
    }

    public void StartAnim()
    {
        StartCoroutine(WaitAndBehave());
    }

    public void Reset()
    {
        StopAllCoroutines();
        progress = 0;
    }
}
