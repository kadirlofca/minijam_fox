using UnityEngine;

public enum CoinTossState
{
    NotTossed,
    Back,
    Forward
}

public struct Turn
{
    bool side;
    CoinTossState coinTossResult;
    int lastProgress;
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
    }
}