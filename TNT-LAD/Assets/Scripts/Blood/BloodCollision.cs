using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCollision : MonoBehaviour
{
  public ParticleSystem PSystem;
  //public GameObject bloodSplash;
  public ParticleSystem bloodSplash;
  private List<ParticleCollisionEvent> CollisionEvents;


  public void OnParticleCollision(GameObject other)
  {
    CollisionEvents = new List<ParticleCollisionEvent>();


    int eventCount = PSystem.GetCollisionEvents(other, CollisionEvents);

    foreach(ParticleCollisionEvent collision in CollisionEvents)
    {
      Instantiate(bloodSplash, collision.intersection, Quaternion.identity);
    }

  }
}
