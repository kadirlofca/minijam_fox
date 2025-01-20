using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

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
    public float behaveDuration = 4;

    [SerializeField] private SceneData sceneData;

    public BoardSlot slotOne;
    public BoardSlot slotTwo;
    public BoardSlot slotThree;
    public BoardSlot slotFour;
    public BoardPiece OpponentPiece;
    public BoardPiece AllyPiece;

    public AudioSource susRising;
    public AudioSource distractionAudio;
    public AudioClip disone;
    public AudioClip distwo;
    public AudioClip disthree;
    public AudioClip disfour;
    public AudioClip disve;
    int distractionProgress = 0;

    public int lastKnownAllyProgress = 0;
    public CoinTossState lastKnownAllyCoinState = CoinTossState.NotTossed;

    public CoinTossAnimation tossAnim;

    public OponentInfoSO oponentInfoSO;

    public float SusLevel = 1f;

    public OpponentBehavior CurrentBehavior = OpponentBehavior.Idle;

    public static Opponent Instance;

    Sprite BehaviorToTexture(OpponentBehavior Behavior)
    {
        Sprite newSprite = oponentInfoSO.Idle;
        switch (Behavior)
        {
            case OpponentBehavior.Idle:
                newSprite = oponentInfoSO.Idle;
                break;
            case OpponentBehavior.Distracted:
                newSprite = oponentInfoSO.Distracted;
                break;
            case OpponentBehavior.Suspicious:
                newSprite = oponentInfoSO.Suspicious;
                break;
            case OpponentBehavior.Confrontational:
                newSprite = oponentInfoSO.Confrontational;
                break;
            default:
                newSprite = oponentInfoSO.Idle;
                break;
        }

        return newSprite;
    }

    OpponentBehavior SusToBehavior()
    {
        if (SusLevel > 3)
        {
            return OpponentBehavior.Confrontational;
        }
        else if (SusLevel > 2)
        {
            return OpponentBehavior.Suspicious;
        }

        return OpponentBehavior.Idle;
    }

    void ChangeBehavior(OpponentBehavior NewBehavior)
    {
        if (CurrentBehavior != OpponentBehavior.Confrontational && NewBehavior == OpponentBehavior.Confrontational)
        {
            susRising.Play();
        }
        else if (CurrentBehavior != OpponentBehavior.Distracted && NewBehavior == OpponentBehavior.Distracted)
        {
            if (distractionProgress == 0)
            {
                distractionAudio.PlayOneShot(disone);
            }
            else if (distractionProgress == 1)
            {
                distractionAudio.PlayOneShot(distwo);
            }
            else if (distractionProgress == 2)
            {
                distractionAudio.PlayOneShot(disthree);
            }
            else if (distractionProgress == 3)
            {
                distractionAudio.PlayOneShot(disfour);
            }
            else
            {
                distractionAudio.PlayOneShot(disve);
            }

            distractionProgress++;
            if (distractionProgress >= 3)
            {
                distractionProgress = 0;
            }
        }

        CurrentBehavior = NewBehavior;
        Renderer.sprite = BehaviorToTexture(NewBehavior);
    }

    void Behave()
    {
        if (lastKnownAllyProgress < 2 && AllyPiece.currentSlot && AllyPiece.currentSlot.ProgressValue == 3)
        {
            SusLevel = 10;
            BoardSlot slottt = slotOne;

            if (lastKnownAllyProgress == 0)
            {
                slottt = slotOne;
            }
            else if (lastKnownAllyProgress == 1)
            {
                slottt = slotTwo;
            }

            AllyPiece.transform.position = slottt.SlotPosition.position;
            AllyPiece.currentSlot = slottt;
            AllyPiece.OnPlaced();
        }

        if (lastKnownAllyCoinState != CoinTossState.Forward && AllyPiece.currentSlot && AllyPiece.currentSlot.ProgressValue > lastKnownAllyProgress + 1)
        {
            SusLevel = 4;
        }

        if (BoardState.Instance.turn.side && !tossAnim.beingTossed)
        {
            lastKnownAllyProgress = AllyPiece.currentSlot ? AllyPiece.currentSlot.ProgressValue : 0;
            lastKnownAllyCoinState = BoardState.Instance.turn.side ? CoinTossState.NotTossed : BoardState.Instance.turn.coinTossResult;

            OpponentBehavior newBehavior = SusToBehavior();
            ChangeBehavior(newBehavior);
            tossAnim.Reset();
            tossAnim.Toss();

            return;
        }

        if (Random.Range(0, 100) > SusLevel * 33)
        {
            ChangeBehavior(OpponentBehavior.Distracted);
        }
        else
        {
            lastKnownAllyProgress = AllyPiece.currentSlot ? AllyPiece.currentSlot.ProgressValue : 0;
            lastKnownAllyCoinState = BoardState.Instance.turn.side ? CoinTossState.NotTossed : BoardState.Instance.turn.coinTossResult;


            OpponentBehavior newBehavior = SusToBehavior();
            ChangeBehavior(newBehavior);
        }
    }

    public void MovePiece(bool forward)
    {
        OpponentPiece.currentSlot.ProgressValue = Mathf.Max(OpponentPiece.currentSlot.ProgressValue + (forward ? 1 : -1), 0);
        int newProgress = OpponentPiece.currentSlot.ProgressValue;
        if (newProgress == 0)
        {
            OpponentPiece.transform.position = slotOne.SlotPosition.position;
            OpponentPiece.currentSlot = slotOne;
        }
        else if (newProgress == 1)
        {
            OpponentPiece.transform.position = slotTwo.SlotPosition.position;
            OpponentPiece.currentSlot = slotTwo;
        }
        else if (newProgress == 2)
        {
            OpponentPiece.transform.position = slotThree.SlotPosition.position;
            OpponentPiece.currentSlot = slotThree;
        }
        else if (newProgress == 3)
        {
            OpponentPiece.transform.position = slotFour.SlotPosition.position;
            OpponentPiece.currentSlot = slotFour;
        }

        OpponentPiece.OnPlaced();
    }

    IEnumerator WaitAndBehave()
    {
        yield return new WaitForSeconds(oponentInfoSO.BehaviorDuration * Random.Range(0.8f, 1.2f) / Mathf.Max(SusLevel, 1));

        Behave();

        StartCoroutine(WaitAndBehave());
    }

    private void Awake()
    {
        oponentInfoSO = sceneData.SelectedOponent;

        if (Instance)
        {
            return;
        }

        Instance = this;
    }

    void Start()
    {
        StartCoroutine(WaitAndBehave());
    }

    void Update()
    {
        SusLevel = Mathf.Max(Mathf.Min(SusLevel - Time.deltaTime * 0.065f, 10), 1);
    }
}
