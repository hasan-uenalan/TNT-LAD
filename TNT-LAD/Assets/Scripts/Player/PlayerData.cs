using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
  public int playerIndex;
  public Vector3 spawnPoint;
  //public float health;

  public int score;

  public List<GameObject> placedBombs = new List<GameObject>();
  private int bombCount;
  private int bombStrength;

  private int lifes;
  private bool isDead;

  private bool powerUpMoveBombs;

  void Start()
  {
    initPlayerData();
  }

  public void initPlayerData()
  {
    score = 0;

    bombCount = 1;
    bombStrength = 1;

    lifes = 3;
    isDead = false;

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
      isDead = true;
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
