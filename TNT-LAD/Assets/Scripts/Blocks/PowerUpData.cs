using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpData : MonoBehaviour
{
  public enum PowerUpType
  {
    ADDBOMB,
    KICKBOMBS,
    STRENGTHBOMB,
    SPEED,
    RPG
  };

  public enum OneTimeUse
  {
    NONE,
    RPG
  }

  public PowerUpType powerUpType;
}
