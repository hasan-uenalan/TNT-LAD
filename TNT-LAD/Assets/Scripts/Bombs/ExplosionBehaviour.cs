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
