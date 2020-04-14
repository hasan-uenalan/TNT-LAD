using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitcher : MonoBehaviour
{
  public void SwitchToGame()
  {
    if(StaticPlayers.Winner != null)
    {
      SceneManager.LoadScene("WinScreen");
      return;
    }
    if (!CrossSceneInformation.gameStarted)
    {
      AnalyticsHandler.GameStartEvent(CrossSceneInformation.PlayerList.Count);
      CrossSceneInformation.gameStarted = true;
    }
    SceneManager.LoadScene("Level");
  }

  public void SwitchToMainMenu()
  {
    SceneManager.LoadScene("MainMenu");
  }

  public void ResetToMainMenu()
  {
    ResetStatics();
    SceneManager.LoadScene("MainMenu");
  }

  public void SwitchToOptions()
  {
    SceneManager.LoadScene("OptionsScene");
  }

  public void SwitchToControls()
  {
    SceneManager.LoadScene("ControlsScene");
  }

  public void SwitchToLeveleditor()
  {
    SceneManager.LoadScene("Editor");
  }

  private void ResetStatics()
  {
    CrossSceneInformation.PlayerList = new List<LobbyPlayerValues>();
    CrossSceneInformation.currentLevel = new LevelInfo();
    CrossSceneInformation.gameStarted = false;
  }
}
