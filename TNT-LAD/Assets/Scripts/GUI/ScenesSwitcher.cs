using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitcher : MonoBehaviour
{
  public void SwitchToGame()
  {
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

  public void SwitchToPlayerSettings()
  {
    SceneManager.LoadScene("PlayerJoinScreen");
  }

  public void SwitchToControls()
  {
    SceneManager.LoadScene("ControlsScene");
  }

  public void SwitchToLevelSettings()
  {
    SceneManager.LoadScene("LevelSelectionScene");
  }

  private void ResetStatics()
  {
    GUIValues.PlayerList = new List<LobbyPlayerValues>();
    GUIValues.LevelName = "";
  }
  
  public void SwitchToCloudLevelSelection()
  {
    SceneManager.LoadScene("CloudLevelSelectionScene");
  }
}
