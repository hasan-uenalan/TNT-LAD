using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GUIValues
{
  private static int levelNumber { get; set; }

  private static List<LobbyPlayerValues> playerList { get; set; }

  public static void SetLevelNumber(int value)
  {
    levelNumber = value;
  }

  public static int GetLevelNumber()
  {
    return levelNumber;
  }

  public static void SetPlayerDict(Dictionary<int, LobbyPlayerValues> value)
  {
    playerDict = value;
  }

  public static Dictionary<int, LobbyPlayerValues> GetPlayerDict()
  {
    return playerDict;
  }
}
