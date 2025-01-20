using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Texture2D cursorTexture;

    public CoinTossAnimation tossAnim;

    public BoardPiece HeldBoardPiece;
    public BoardSlot HoveredBoardSlot;
    public static Player Instance;
    Vector3 DragOffset;
    public HandAnimation handAnim;

    void Awake()
    {
        if (Instance)
        {
            return;
        }

        Instance = this;
    }

    RaycastHit[] GetGameObjectAtPosition()
    {
        LayerMask mask = ~0;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.RaycastAll(ray, 999999, mask, QueryTriggerInteraction.Ignore);
    }

    void GrabBoardPiece()
    {
        foreach (RaycastHit hit in GetGameObjectAtPosition())
        {
            BoardPiece DetectedBoardPiece;
            if (hit.transform.gameObject.TryGetComponent<BoardPiece>(out DetectedBoardPiece))
            {
                HeldBoardPiece = DetectedBoardPiece;
                DragOffset = HeldBoardPiece.transform.position - GetMousePosition();

                HeldBoardPiece.GetComponent<SpriteRenderer>().sortingOrder = 10;
            }
        }
    }

    void TossCoin()
    {
        tossAnim.Reset();
        handAnim.Reset();
        handAnim.StartAnim();
        tossAnim.Toss();
    }

    public void OnMouseAction(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            GrabBoardPiece();

            if (!HeldBoardPiece && BoardState.Instance.turn.side == false && BoardState.Instance.turn.coinTossResult == CoinTossState.NotTossed)
            {
                TossCoin();
            }

            return;
        }

        if (HeldBoardPiece)
        {
            if (HoveredBoardSlot && HoveredBoardSlot.BoardSide == HeldBoardPiece.side)
            {
                // ON PLACED INTO SLOT
                int oldProgress = HeldBoardPiece.currentSlot ? HeldBoardPiece.currentSlot.ProgressValue : 0;
                HoveredBoardSlot.UnHighlight();
                HeldBoardPiece.transform.position = HoveredBoardSlot.SlotPosition.position;
                HeldBoardPiece.currentSlot = HoveredBoardSlot;
                HeldBoardPiece.OnPlaced();

                if (Opponent.Instance.CurrentBehavior != OpponentBehavior.Distracted)
                {
                    if (HeldBoardPiece.currentSlot.ProgressValue > oldProgress && Opponent.Instance.lastKnownAllyCoinState != CoinTossState.Forward)
                    {
                        Opponent.Instance.SusLevel += 1f;
                    }
                }

                if (!(oldProgress == HeldBoardPiece.currentSlot.ProgressValue) && (HeldBoardPiece.side || BoardState.Instance.turn.side || BoardState.Instance.turn.coinTossResult == CoinTossState.NotTossed))
                {
                    Opponent.Instance.SusLevel += 1f;
                }

                if (!BoardState.Instance.turn.side && BoardState.Instance.turn.coinTossResult != CoinTossState.NotTossed)
                {
                    BoardState.Instance.EndTurn();
                }
            }
            else
            {
                HeldBoardPiece.transform.position = HeldBoardPiece.currentSlot.SlotPosition.position;
            }

            HeldBoardPiece.GetComponent<SpriteRenderer>().sortingOrder = 1;
            HeldBoardPiece = null;
        }
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return mousePosition;
    }

    void DetectSlot()
    {
        BoardSlot DetectedSlot = null;

        foreach (RaycastHit hit in GetGameObjectAtPosition())
        {
            if (hit.transform.gameObject.TryGetComponent(out BoardSlot tempSlot))
            {
                DetectedSlot = hit.transform.gameObject.GetComponent<BoardSlot>();
            }
        }

        if (HoveredBoardSlot && HoveredBoardSlot != DetectedSlot)
        {
            HoveredBoardSlot.UnHighlight();
        }

        HoveredBoardSlot = DetectedSlot;
    }

    void Update()
    {
        DetectSlot();

        if (HeldBoardPiece && HoveredBoardSlot && HoveredBoardSlot.BoardSide == HeldBoardPiece.side && HeldBoardPiece)
        {
            HoveredBoardSlot.Highlight();
        }

        if (HeldBoardPiece)
        {
            HeldBoardPiece.transform.position = GetMousePosition() + DragOffset;
        }
    }
}