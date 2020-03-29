using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler
{
  public void HandlePowerUp(GameObject powerUp, PlayerData playerData, GameObject thisGameObject)
  {
    switch (powerUp.GetComponent<PowerUpData>().powerUpType)
    {
      case PowerUpData.PowerUpType.ADDBOMB:
        playerData.BombCount += 1;
        break;
      case PowerUpData.PowerUpType.KICKBOMBS:
        playerData.KickBombs = true;
        break;
      case PowerUpData.PowerUpType.STRENGTHBOMB:
        playerData.BombStrength += 1;
        break;
      case PowerUpData.PowerUpType.SPEED:
        playerData.PlayerSpeed += .5f;
        thisGameObject.GetComponent<PlayerMovement>().walkSpeed = playerData.PlayerSpeed; //updating playerspeed
        break;
      case PowerUpData.PowerUpType.RPG:
        playerData.oneTimeUse = PlayerData.OneTimeUse.RPG;
        break;
    }
  }

}
