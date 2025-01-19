using UnityEngine;

public enum CoinTossState
{
    NotTossed,
    Back,
    Forward
}

public struct Turn
{
    public bool side;
    public CoinTossState coinTossResult;
    public int lastProgress;
}

public class BoardState : MonoBehaviour
{
    public static BoardState Instance;

    public int AllyBoardProgress = 0;
    public int OpponentBoardProgress = 0;
    public Turn turn;

    void Awake()
    {
        if (Instance)
        {
            return;
        }

        Instance = this;

        // start from opponent
        turn.side = true;
        turn.lastProgress = 0;
    }

    void OpponentMove()
    {

    }

    void OpponentTossCoin()
    {
    }

    void EndTurn()
    {
        turn.side = !turn.side;
        turn.coinTossResult = CoinTossState.NotTossed;
        turn.lastProgress = turn.side ? OpponentBoardProgress : AllyBoardProgress;
    }
}