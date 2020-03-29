using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDropper : MonoBehaviour
{
  public GameObject bomb;
  private PlayerHandler playerHandler;

  private void Start()
  {
    playerHandler = gameObject.GetComponent<PlayerHandler>();
  }

  void OnDropBomb()
  {
    if (playerHandler.CanPlaceBombs())
    {
      Vector3 placementPosition = new Vector3(
          Mathf.RoundToInt(gameObject.transform.position.x), 
          0.5f, 
          Mathf.RoundToInt(gameObject.transform.position.z)
        );
      foreach(var collider in Physics.OverlapSphere(placementPosition, 0.01f))
      {
        if(collider.gameObject.tag == "Bomb")
        {
          Debug.Log($"Bomb already on: {placementPosition}");
          return; //Don't drop Bomb if one is already there.
        }
        if(collider.gameObject.tag == "Explosion")
        {
          Debug.Log("Bomb cannot be placed inside an explosion!");
          return;
        }
      }
      var bombGameObject = Instantiate(bomb, placementPosition, Quaternion.identity);
      bombGameObject.GetComponent<BombExploder>().explosionRange = playerHandler.PlayerData.BombStrength;
      playerHandler.PlacedBombs.Add(bombGameObject);
    }
    else
    {
      Debug.Log("Max amount bombs are set");
    }
    
    //bombGameObject.GetComponent<BombExploder>().bombOwner = gameObject;
  }
}
