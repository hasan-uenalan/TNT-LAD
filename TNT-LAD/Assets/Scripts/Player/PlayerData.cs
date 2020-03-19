using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
  public int PlayerScore;

  public int PlayerIndex;

  public float InvincibilityTime;

  public int Lifes;

  public Vector3 SpawnPoint;

  /// <summary>
  /// Instantiates Values for a player
  /// </summary>
  /// <param name="playerIndex"></param>
  /// <param name="playerScore"></param>
  /// <param name="invincibilityTime"></param>
  /// <param name="lifes"></param>
  public PlayerData(int playerIndex, int playerScore, float invincibilityTime, int lifes)
  {
    PlayerIndex = playerIndex;
    PlayerScore = playerScore;
    InvincibilityTime = invincibilityTime;
    Lifes = lifes;
  }
}
