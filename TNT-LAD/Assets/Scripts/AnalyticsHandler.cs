﻿using System.Collections.Generic;
using UnityEngine.Analytics;

public static class AnalyticsHandler
{
  public static void LevelSelectEvent(bool isLocalLevel)
  {
    var parameters = new Dictionary<string, object>();
    string levelType = isLocalLevel ? EventConstants.LevelTypeLocal : EventConstants.LevelTypeCloud;
    parameters.Add(EventConstants.LevelType, levelType);
    Analytics.CustomEvent(EventConstants.LevelSelect, parameters);
  }

  public static void GameStartEvent(int playerNumber)
  {
    var parameters = new Dictionary<string, object>();
    parameters.Add(EventConstants.PlayersInParty, playerNumber);
    Analytics.CustomEvent(EventConstants.GameStart, parameters);
  }
}

public static class EventConstants
{
  public const string LevelSelect = "Level Selected";
  public const string LevelType = "Level Type";
  public const string LevelTypeLocal = "Local Level";
  public const string LevelTypeCloud = "Cloud Level";
  
  public const string GameStart = "Game Started";
  public const string PlayersInParty = "Players in Party";
}
