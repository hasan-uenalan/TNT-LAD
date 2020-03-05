using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
  public enum status 
  {
    alive,
    dead,
    invincible
  };

  public int playerIndex;
  public Vector3 spawnPoint;
  public float invincibilityTime;

  //Bomb data
  public List<GameObject> placedBombs = new List<GameObject>();
  public int bombCount { get; set; }
  public int bombStrength { get; set; }

  /// <summary>
  /// player status data
  /// </summary>
  public int lifes { get; set; }

  public bool powerUpMoveBombs;

  public status playerStatus { get; set; }

  void Start()
  {
    initPlayerData();
    StaticPlayers.staticPlayers.Add(new StaticPlayerData(playerIndex,0));
  }

  public void initPlayerData()
  { 
    bombCount = 1;
    bombStrength = 1;
    playerStatus = status.alive;
    lifes = 3;

    powerUpMoveBombs = false;
  }

  //checks if player can place more bombs
  public bool CanPlaceBombs()
  {
    if(placedBombs.Count < bombCount)
    {
      return true;
    }
    return false;
  }
  //removes a life and checks if player is dead
  public void RemoveLife()
  {
    lifes -= 1;
    if(lifes == 0)
    {
      KillPlayer();
    }
    Debug.Log("lifes: " + lifes);
    SetInvincible();
  }

  private void KillPlayer()
  {
    playerStatus = status.dead;
    gameObject.SetActive(false);
  }

  private void SetInvincible()
  {
    this.playerStatus = status.invincible;
    Debug.Log("player is invincible");
    if (isActiveAndEnabled) //start only if the player is active
    {
      StartCoroutine(setStatusTime(status.alive, invincibilityTime));
    }
  }

  IEnumerator setStatusTime(PlayerData.status playerStatus, float delay)
  {
    yield return new WaitForSeconds(delay);
    this.playerStatus = playerStatus;
    Debug.Log("player not invincible anymore");
  }

}
