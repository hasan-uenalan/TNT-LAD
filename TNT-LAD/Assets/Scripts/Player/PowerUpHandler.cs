﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler
{
  public void HandlePowerUp(GameObject powerUp, PlayerData playerData)
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
        Debug.Log(playerData.BombStrength);
        break;
      case PowerUpData.PowerUpType.SPEED:
        playerData.PlayerSpeed += 1;
        break;
      case PowerUpData.PowerUpType.RPG:
        playerData.oneTimeUse = PlayerData.OneTimeUse.RPG;
        break;
    }
  }

}
