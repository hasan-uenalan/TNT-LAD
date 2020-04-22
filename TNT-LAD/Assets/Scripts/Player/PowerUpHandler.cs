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
        playerData.oneTimeUse = PowerUpData.OneTimeUse.RPG;
        thisGameObject.GetComponentInChildren<PowerUpSwitcher>().selectedPowerUp = 1;
        break;
    }
  }

  public void HandleOneTimeUse(PowerUpData.OneTimeUse oneTimeUse, GameObject thisGameObject)
  {
    switch (oneTimeUse)
    {
      case PowerUpData.OneTimeUse.RPG:
        SoundManager.PlaySound(SoundManager.Sound.RPG);
        thisGameObject.GetComponentInChildren<RPGPowerUp>().Use();
        thisGameObject.GetComponentInChildren<PowerUpSwitcher>().selectedPowerUp = 0;
        break;
    }
  }

}
