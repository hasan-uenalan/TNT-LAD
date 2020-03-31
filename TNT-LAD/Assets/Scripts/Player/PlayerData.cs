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

  public int PlayerScore { get; set; }
  public int PlayerIndex { get; set; }
  public Color PlayerColor { get; set; }
  public float InvincibilityTime { get; set; }
  public int Lifes { get; set; }
  public Vector3 SpawnPoint { get; set; }
  public OneTimeUse oneTimeUse { get; set; }
  public int BombCount { get; set; }
  public int BombStrength { get; set; }
  public int PlayerSpeed { get; set; }
  public bool KickBombs { get; set; }


  /// <summary>
  /// Instantiates Values for a player
  /// </summary>
  /// <param name="playerIndex"></param>
  /// <param name="playerScore"></param>
  /// <param name="invincibilityTime"></param>
  /// <param name="lifes"></param>
  public PlayerData(int playerIndex, int playerScore, float invincibilityTime, int lifes, Color playerColor, Vector3 spawnPoint)
  {
    PlayerIndex = playerIndex;
    PlayerScore = playerScore;
    PlayerColor = playerColor;
    InvincibilityTime = invincibilityTime;
    Lifes = lifes;
    SpawnPoint = spawnPoint;
    BombCount = 1;
    BombStrength = 2;
    PlayerSpeed = 2;
    KickBombs = false;
    oneTimeUse = OneTimeUse.NONE;
  }
}
