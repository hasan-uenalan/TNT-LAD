using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CrossSceneInformation
{
  public static LevelInfo currentLevel { get; set; } = new LevelInfo();
  public static List<LobbyPlayerValues> PlayerList { get; set; }
  public static int RoundTime { get; set; } = 300; //TODO: File
  public static int PlayerLifes { get; set; } = 3; //TODO: File

}
