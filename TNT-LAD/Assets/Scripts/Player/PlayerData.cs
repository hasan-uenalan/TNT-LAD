using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
  public enum OneTimeUse
  {
    NONE,
    RPG
  }

  public int PlayerScore;

  public int PlayerIndex;

  public float InvincibilityTime;

  public int Lifes;

  public Vector3 SpawnPoint;
  public OneTimeUse oneTimeUse { set; get; }
  public int BombCount { set; get; }
  public int BombStrength { set; get; }
  public int PlayerSpeed { set; get; }
  public bool KickBombs { set; get; }


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
    BombCount = 1;
    BombStrength = 1;
    PlayerSpeed = 1;
    KickBombs = false;
    oneTimeUse = OneTimeUse.NONE;
  }
}
