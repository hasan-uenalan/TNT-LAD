using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincibility : MonoBehaviour
{
  public GameObject shieldGroup;
  public float rotationSpeed;
  private PlayerHandler playerHandler;
  private Quaternion fixRotation; 

  void Start()
  {
    playerHandler = GetComponent<PlayerHandler>();
  }

  void Update()
  {
    if(playerHandler.PlayerStatus == PlayerHandler.Status.invincible)
    {
      shieldGroup.SetActive(true);
      shieldGroup.transform.rotation = fixRotation;
      shieldGroup.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
      fixRotation = shieldGroup.transform.rotation;
      return;
    }
    shieldGroup.SetActive(false);
  }
}
