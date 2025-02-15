using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private WonAgainstSO _wonAgainst;
    [SerializeField] private SceneData _sceneData;

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

    public void EndTurn()
    {
        turn.side = !turn.side;
        turn.coinTossResult = CoinTossState.NotTossed;

        // if winning condition
        if (AllyBoardProgress == 3 || OpponentBoardProgress == 3)
        {
            if (Opponent.Instance.CurrentBehavior == OpponentBehavior.Confrontational)
            {
                SceneManager.LoadScene(1);
            }

            if (AllyBoardProgress == 3)
            {
                PlayerWon();
                CheckWhereToLoad();

            }
        }
    }

    void CheckWhereToLoad()
    {
        // Switch the number for the amount of available opponents!!
        if (_wonAgainst.Oponents.Count == 1)
        {
            //loads into "win scene"
            SceneManager.LoadScene(3);
        }

        // loads into opponent selection
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Call whenever the player wins!!
    /// </summary>
    void PlayerWon()
    {
        _wonAgainst.AddOponent(_sceneData.SelectedOponent);
    }
}