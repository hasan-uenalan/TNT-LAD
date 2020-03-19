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
  public Vector3 SpawnPoint;
  public float InvincibilityTime;

  //Bomb data
  public List<GameObject> PlacedBombs = new List<GameObject>();
  public int BombCount { get; set; }
  public int BombStrength { get; set; }

  /// <summary>
  /// player status data
  /// </summary>
  public int Lifes { get; set; }

  public bool PowerUpMoveBombs;

  public Status PlayerStatus { get; set; }

  void Start()
  {
    InitPlayerData();
    StaticPlayers.staticPlayers.Add(PlayerValues);
  }

  public void InitPlayerData()
  {
    //PlayerIndex has to be set
    PlayerValues = new PlayerData( 1, 0, SpawnPoint, InvincibilityTime);
    BombCount = 1;
    BombStrength = 1;
    PlayerStatus = Status.alive;
    Lifes = 3;

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
    Lifes -= 1;
    if(Lifes == 0)
    {
      KillPlayer();
    }
    Debug.Log("lifes: " + Lifes);
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
