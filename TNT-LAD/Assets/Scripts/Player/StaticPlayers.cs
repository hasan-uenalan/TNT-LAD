using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticPlayers
{
  public static List<PlayerData> Players = new List<PlayerData>();

  public static bool roundOne = true;

  public static void SetLifes(int lifes)
  {
    foreach (PlayerData curPlayer in Players) {
      curPlayer.Lifes = lifes;
    }
  }
}
