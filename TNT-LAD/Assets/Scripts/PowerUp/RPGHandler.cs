using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGHandler : MonoBehaviour
{
  public float speed = 2;
  public Transform trail;
  public ParticleSystem trailEffect;

  private void Start()
  {
    Destroy(gameObject, 5f); // Destroys if it doesn't hit anything
  }

  // Update is called once per frame
  void Update()
  {
    transform.position += -transform.up * speed * Time.deltaTime;
    Instantiate(trailEffect, trail.transform.position, Quaternion.identity);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      var playerData = other.gameObject.GetComponent<PlayerHandler>();
      if (playerData.PlayerStatus != PlayerHandler.Status.invincible)
      {
        other.gameObject.GetComponent<PlayerHandler>().RemoveLife(gameObject.transform.position);
      }
    }
    BlockData blockdata;
    if (other.gameObject.TryGetComponent<BlockData>(out blockdata))
    {
      if (blockdata.blockType == BlockData.BlockType.DESTRUCTIBLE)
      {
        other.gameObject.GetComponent<BoxHandler>()
          .DestroyBoxAndSpawnPowerUp();
      }
    }
    Destroy(gameObject);
  }
}
