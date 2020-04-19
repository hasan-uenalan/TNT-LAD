using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkRocket : MonoBehaviour
{
  public float delay;

  public float speed;
  public float duration;
  public GameObject fireworkExplosion;

  private bool moveRocket = false;

  void Start()
  {
    Invoke("StartRocket", delay);  
  }

  void StartRocket()
  {
    moveRocket = true;
    gameObject.GetComponent<ParticleSystem>().Play();
  }

  void FixedUpdate()
  {
    if (moveRocket)
    {
      gameObject.transform.position += new Vector3(0, speed * Time.fixedDeltaTime, 0);
      duration -= Time.fixedDeltaTime;
      if (duration <= 0)
      {
        Instantiate(fireworkExplosion, gameObject.transform.position, Quaternion.identity);
        gameObject.GetComponent<ParticleSystem>().Stop();
        Destroy(gameObject, 2f);
        moveRocket = false;
      }
    }
  }
}
