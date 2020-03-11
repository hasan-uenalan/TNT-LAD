using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCollision : MonoBehaviour
{

  public ParticleSystem PSystem;
  private List<ParticleCollisionEvent> CollisionEvents;


  public void OnParticleCollision(GameObject other)
  {
    CollisionEvents = new List<ParticleCollisionEvent>();


    int eventCount = PSystem.GetCollisionEvents(other, CollisionEvents);

    foreach(ParticleCollisionEvent collision in CollisionEvents)
    {
      Debug.Log(collision.intersection);
    }

  }
}
