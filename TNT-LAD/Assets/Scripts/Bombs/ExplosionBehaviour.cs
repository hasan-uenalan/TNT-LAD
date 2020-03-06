using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
  [Header("Sphere Collider")]
  public float explosionRadius;
  public float expansionSpeed;
  public float expansionOnsetDelay;

  [Header("Point Light")]
  public float maxLightIntensity;
  public float lightFadeSpeed;
  
  private SphereCollider explosionCollider;
  private Light pointLight;
  private bool isColliderExpanding;
  private bool isLightAnimating;

  //Components
  private PlayerData playerData;

  void Start()
  {
    explosionCollider = gameObject.GetComponent<SphereCollider>();
    pointLight = gameObject.GetComponent<Light>();
    Invoke("StartColliderExpansion", expansionOnsetDelay);
    Invoke("StartLightAnimation", 0);
  }

  private void Update()
  {
    if (isColliderExpanding)
    {
      DoColliderExpansion();
    }
    if (isLightAnimating)
    {
      DoLightAnimation();
    }
  }

  void DoColliderExpansion()
  {
    //Expand collider unil final radius is reached
    if (explosionCollider.radius < explosionRadius)
    {
      explosionCollider.radius += Time.deltaTime * expansionSpeed;
      return;
    }
    StopColliderExpansion();
  }

  void DoLightAnimation()
  {
    //Fade light after explosion
    if(pointLight.intensity >= 0)
    {
      pointLight.intensity -= Time.deltaTime * lightFadeSpeed;
      return;
    }
    StopLightAnimation();
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

  private void StartColliderExpansion()
  {
    explosionCollider.enabled = true;
    isColliderExpanding = true;
  }

  private void StopColliderExpansion()
  {
    isColliderExpanding = false;
    explosionCollider.enabled = false;
  }

  private void StartLightAnimation()
  {
    pointLight.intensity = maxLightIntensity;
    pointLight.enabled = true;
    isLightAnimating = true;
  }

  private void StopLightAnimation()
  {
    isLightAnimating = false;
    pointLight.enabled = false;
  }
}
