using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
  public enum Status 
  {
    alive,
    dead,
    invincible
  };

  public PlayerData PlayerValues;

  //Bomb data
  public List<GameObject> PlacedBombs = new List<GameObject>();
  public int BombCount { get; set; }
  public int BombStrength { get; set; }

  /// <summary>
  /// player status data
  /// </summary>
  public bool PowerUpMoveBombs;

  public Status PlayerStatus { get; set; }

  void Start()
  {
    InitPlayerData();
  }

  public void InitPlayerData()
  {
    //PlayerIndex has to be set
    BombCount = 1;
    BombStrength = 1;
    PlayerStatus = Status.alive;

    PowerUpMoveBombs = false;
  }

  //checks if player can place more bombs
  public bool CanPlaceBombs()
  {
    if(PlacedBombs.Count < BombCount)
    {
      return true;
    }
    return false;
  }
  //removes a life and checks if player is dead
  public void RemoveLife()
  {
    PlayerValues.Lifes -= 1;
    if(PlayerValues.Lifes == 0)
    {
      KillPlayer();
    }
    Debug.Log("lifes: " + PlayerValues.Lifes);
    SetInvincible();
  }

  private void KillPlayer()
  {
    PlayerStatus = Status.dead;
    gameObject.SetActive(false);
    gameObject.GetComponent<PlayerKiller>().KillPlayer(false);
  }

  private void SetInvincible()
  {
    this.PlayerStatus = Status.invincible;
    Debug.Log("player is invincible");
    if (isActiveAndEnabled) //start only if the player is active
    {
      StartCoroutine(SetStatusTime(Status.alive, PlayerValues.InvincibilityTime));
    }
  }

  IEnumerator SetStatusTime(PlayerHandler.Status playerStatus, float delay)
  {
    yield return new WaitForSeconds(delay);
    this.PlayerStatus = playerStatus;
    Debug.Log("player not invincible anymore");
  }
}
