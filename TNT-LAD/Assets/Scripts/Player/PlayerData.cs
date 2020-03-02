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
  //public float health;

  private int score { get; set; }

  //Bomb data
  public List<GameObject> placedBombs = new List<GameObject>();
  private int bombCount { get; set; }
  private int bombStrength { get; set; }

  //player status data
  private int lifes { get; set; }

  private bool powerUpMoveBombs;

  private status playerStatus { get; set; }

  void Start()
  {
    initPlayerData();
  }

  public void initPlayerData()
  {
    score = 0;

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
      playerStatus = status.dead;
    }
  }


  //void Update()
  //{
  //  if(health <= 0 && !isDead)
  //  {
  //    isDead = true;
  //    //Kill player
  //  }
  //}

  //void Respawn()
  //{
  //  gameObject.transform.position = spawnPoint;
  //  health = 100;
  //  isDead = false;
  //}
}
