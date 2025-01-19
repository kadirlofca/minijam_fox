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
    public SpriteRenderer Renderer;
    public Sprite Idle;
    public Sprite Distracted;
    public Sprite Suspicious;
    public Sprite Confrontational;

    public float SusLevel = 0;
    public float BehaviorDuration = 10;
    public OpponentBehavior CurrentBehavior = OpponentBehavior.Idle;

    Sprite BehaviorToTexture(OpponentBehavior Behavior)
    {
        switch (Behavior)
        {
            case OpponentBehavior.Idle:
                return Idle;
            case OpponentBehavior.Distracted:
                return Distracted;
            case OpponentBehavior.Suspicious:
                return Suspicious;
            case OpponentBehavior.Confrontational:
                return Confrontational;
            default:
                return Idle;
        }
    }

    void ChangeBehavior(OpponentBehavior NewBehavior)
    {
        CurrentBehavior = NewBehavior;
        Renderer.sprite = BehaviorToTexture(NewBehavior);
    }

    void Behave()
    {
        if (Random.Range(0, 1) > 0)
        {
        }

        ChangeBehavior(OpponentBehavior.Distracted);

        if (CurrentBehavior == OpponentBehavior.Distracted)
        {

        }
        else if (SusLevel > 1)
        {
            CurrentBehavior = OpponentBehavior.Suspicious;
        }
        else if (SusLevel > 2)
        {
            CurrentBehavior = OpponentBehavior.Confrontational;
        }
    }

    IEnumerator WaitAndBehave()
    {
        yield return new WaitForSeconds(BehaviorDuration);

        Behave();

        StartCoroutine(WaitAndBehave());
    }

    void Start()
    {
        StartCoroutine(WaitAndBehave());
    }
}
