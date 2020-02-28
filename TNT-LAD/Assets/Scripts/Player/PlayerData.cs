using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
  public int playerIndex;
  public float health;
  public int score;
  public List<GameObject> placedBombs = new List<GameObject>();
  public Vector3 spawnPoint;

  private bool isDead;

  //void Start()
  //{
  //  health = 100;
  //}

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
