using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentEvents : MonoBehaviour
{
  public Vector3 bombSpawn1;
  public Vector3 bombSpawn2;
  public GameObject bomb;
  public GameObject destructibleBox;

  void Start()
  {
    StartCoroutine(SpawnBombRoutine());
  }

  IEnumerator SpawnBombRoutine()
  {
    while (true)
    {
      yield return new WaitForSeconds(Random.Range(4, 8));
      Vector3 spawn = (Random.Range(0, 2) == 0) ? bombSpawn1 : bombSpawn2;
      Instantiate(destructibleBox, spawn, Quaternion.identity);
      GameObject bombObj = Instantiate(bomb, spawn, Quaternion.identity);
      if(bombObj.TryGetComponent(out BombExploder bombExploder))
      {
        bombExploder.fuseTimeSeconds = 0;
        bombExploder.explosionRange = Random.Range(2, 8);
      }
    }
  }
}
