using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinToss : MonoBehaviour
{
     public static Func<bool> OnEnemyTossCoin;
     public static Func<bool> OnPlayerTossCoin;
     
     [SerializeField] private SceneData sceneData;
     
     private OponentInfoSO currentOponent;

     private void Awake()
     {
          currentOponent = sceneData.SelectedOponent;
     }

     private void OnEnable()
     {
          OnEnemyTossCoin += EnemyTossCoin;
          OnPlayerTossCoin += PlayerTossCoin;
     }

     private void OnDisable()
     {
          OnEnemyTossCoin -= EnemyTossCoin;
          OnPlayerTossCoin -= PlayerTossCoin;
     }

     /// <summary>
     /// Returns true if the enemy wins
     /// </summary>
     private bool EnemyTossCoin()
     {
          return TossCoin(currentOponent.OponentWinChance);
     }
     
     /// <summary>
     /// Returns true if the player wins
     /// </summary>
     private bool PlayerTossCoin()
     {
          return TossCoin(currentOponent.OponentWinChance);
     }

     bool TossCoin(float winRate)
     {
          float randomValue = Random.Range(0, 100);
          
          return randomValue < winRate;
     }
}
