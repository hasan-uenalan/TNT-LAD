using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitcher : MonoBehaviour
{
  public void SwitchToGame()
  {
    gameObject.GetComponent<SceneLoader>().LoadLevel(GUIValues.LevelName);
  }

  public void SwitchToMainMenu()
  {
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
}
