using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
  public float explosionRadius;
  public float expansionSpeed;
  public float expansionOnsetDelay;
  
  private SphereCollider explosionCollider;
  private bool isExploding;

  //Components
  private PlayerData playerData;

  void Start()
  {
    explosionCollider = gameObject.GetComponent<SphereCollider>();
    Invoke("StartExplosion", expansionOnsetDelay);
  }

  private void Update()
  {
    if (isExploding)
    {
      DoExplosionExpansion();
    }
  }

  void DoExplosionExpansion()
  {
    //Expand collider unil final radius is reached
    if (explosionCollider.radius < explosionRadius)
    {
      explosionCollider.radius += Time.deltaTime * expansionSpeed;
      return;
    }
    StopExplosion();
  }

  private void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      playerData = other.gameObject.GetComponent<PlayerData>();
      if (playerData.playerStatus != PlayerData.status.invincible)
      {
        playerData.RemoveLife();      
      }
    }
    if(other.gameObject.tag == "Bomb")
    {
      BombExploder bombExplosion = other.gameObject.GetComponent<BombExploder>();
      bombExplosion.ExplodeBomb();
    }

    BlockData blockdata;
    if (other.gameObject.TryGetComponent<BlockData>(out blockdata))
    {
      if(blockdata.blockType == BlockData.BlockType.DESTRUCTIBLE)
      {
        other.gameObject.GetComponent<BoxDestroyer>().DestroyBox();
      }
    }
  }

  private void StartExplosion()
  {
    explosionCollider.enabled = true;
    isExploding = true;
  }

  private void StopExplosion()
  {
    isExploding = false;
    explosionCollider.enabled = false;
  }
}
