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

  private int lives;
  private bool isDead;

  private bool powerUpMoveBombs;

  void Start()
  {
    score = 0;

    bombCount = 1;
    bombStrength = 1;

    lives = 3;
    isDead = false;

    powerUpMoveBombs = false;
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
