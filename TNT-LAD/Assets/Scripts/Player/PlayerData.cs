using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
  public int PlayerScore;

  public int PlayerIndex;

  public float InvincibilityTime;

  public Vector3 SpawnPoint;

  public PlayerData(int playerIndex, int playerScore, Vector3 spawnPoint, float invincibilityTime)
  {
    PlayerIndex = playerIndex;
    PlayerScore = playerScore;
    SpawnPoint = spawnPoint;
    InvincibilityTime = invincibilityTime;
  }
}
