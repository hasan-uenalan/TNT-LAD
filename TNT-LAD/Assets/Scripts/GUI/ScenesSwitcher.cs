using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitcher : MonoBehaviour
{
  private MusicHandler musicHandler;

  void Start()
  {
    musicHandler = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicHandler>();
  }

  public void SwitchToGame()
  {
    if (!CrossSceneInformation.gameStarted)
    {
      AnalyticsHandler.GameStartEvent(CrossSceneInformation.PlayerList.Count);
      CrossSceneInformation.gameStarted = true;
    }
    musicHandler.ChangeTrack("InGame");
    SceneManager.LoadScene("Level");
  }

  public void SwitchToMainMenu()
  {
    musicHandler.ChangeTrack("MainMenu");
    SceneManager.LoadScene("MainMenu");
  }

  public void ResetToMainMenu()
  {
    ResetStatics();
    musicHandler.ChangeTrack("MainMenu");
    SceneManager.LoadScene("MainMenu");
  }

  public void SwitchToOptions()
  {
    SceneManager.LoadScene("OptionsScene");
  }

  //public void SwitchToPlayerSettings()
  //{
  //  SceneManager.LoadScene("PlayerJoinScreen");
  //}

  public void SwitchToControls()
  {
    SceneManager.LoadScene("ControlsScene");
  }

  //public void SwitchToLevelSettings()
  //{
  //  SceneManager.LoadScene("LevelSelectionScene");
  //}

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
  
  //public void SwitchToCloudLevelSelection()
  //{
  //  SceneManager.LoadScene("CloudLevelSelectionScene");
  //}
}
