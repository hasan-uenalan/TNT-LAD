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
  

  public PlayerData PlayerData;

  //Bomb data
  public List<GameObject> PlacedBombs = new List<GameObject>();

  //Used Components
  private PowerUpHandler powerUpHandler;

  public Status PlayerStatus { get; set; }

  void Start()
  {
    SetIngameHatColor();
    powerUpHandler = new PowerUpHandler();
    InitPlayerData();
    SetPlayerToSpawn();
  }

  private void Update()
  {
    PlacedBombs.RemoveAll(item => item == null);
  }

  public void SetPlayerToSpawn()
  {
    gameObject.transform.GetComponent<CharacterController>().enabled = false;
    gameObject.transform.position = PlayerData.SpawnPoint;
    gameObject.transform.GetComponent<CharacterController>().enabled = true;
  }

  public void InitPlayerData()
  {
    PlayerStatus = Status.alive;
  }

  private void SetIngameHatColor()
  {
    gameObject.transform.GetComponent<PlayerKiller>().hat.transform.GetComponent<MeshRenderer>().material.color =
      PlayerData.PlayerColor;
  }

  //checks if player can place more bombs
  public bool CanPlaceBombs()
  {
    if(PlacedBombs.Count < PlayerData.BombCount)
    {
      return true;
    }
    return false;
  }
  //removes a life and checks if player is dead
  public void RemoveLife(Vector3 bombPosition)
  {
    PlayerData.Lifes -= 1;
    if(PlayerData.Lifes == 0)
    {
      PlayerStatus = Status.dead;
      KillPlayer(bombPosition);
      return;
    }
    SetInvincible();
  }

  private void KillPlayer(Vector3 bombPosition)
  {
    PlayerStatus = Status.dead;
    //gameObject.SetActive(false);
    gameObject.GetComponent<PlayerKiller>().KillPlayer(false, bombPosition);
  }

  private void SetInvincible()
  {
    this.PlayerStatus = Status.invincible;
    if (isActiveAndEnabled) //start only if the player is active
    {
      StartCoroutine(SetStatusTime(Status.alive, PlayerData.InvincibilityTime));
    }
  }

  IEnumerator SetStatusTime(PlayerHandler.Status playerStatus, float delay)
  {
    yield return new WaitForSeconds(delay);
    this.PlayerStatus = playerStatus;
  }

  private void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "powerup")
    {
      powerUpHandler.HandlePowerUp(other.gameObject, PlayerData, gameObject);
      Destroy(other.gameObject);
    }
  }
}
