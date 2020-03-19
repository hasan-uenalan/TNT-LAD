using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
  public int PlayerScore;

  public int PlayerIndex;

  public Vector3 SpawnPoint;

  private void KillPlayer()
  {
    playerStatus = status.dead;
    //gameObject.SetActive(false);
    gameObject.GetComponent<PlayerKiller>().KillPlayer(false);
  }

  public PlayerData(int playerIndex, int playerScore, Vector3 spawnPoint, float invincibilityTime)
  {
    PlayerIndex = playerIndex;
    PlayerScore = playerScore;
    SpawnPoint = spawnPoint;
    InvincibilityTime = invincibilityTime;
  }
}
