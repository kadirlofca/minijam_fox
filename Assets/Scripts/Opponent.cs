using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

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

    [SerializeField] private SceneData sceneData;

    private OponentInfoSO oponentInfoSO;
    
    public float SusLevel = 0;
    
    public OpponentBehavior CurrentBehavior = OpponentBehavior.Idle;

    Sprite BehaviorToTexture(OpponentBehavior Behavior)
    {
        switch (Behavior)
        {
            case OpponentBehavior.Idle:
                return oponentInfoSO.Idle;
            case OpponentBehavior.Distracted:
                return oponentInfoSO.Distracted;
            case OpponentBehavior.Suspicious:
                return oponentInfoSO.Suspicious;
            case OpponentBehavior.Confrontational:
                return oponentInfoSO.Confrontational;
            default:
                return oponentInfoSO.Idle;
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
        yield return new WaitForSeconds(oponentInfoSO.BehaviorDuration);

        Behave();

        StartCoroutine(WaitAndBehave());
    }

    private void Awake()
    {
        oponentInfoSO = sceneData.SelectedOponent;
    }

    void Start()
    {
        StartCoroutine(WaitAndBehave());
    }
}
