using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControlTransitioner : MonoBehaviour
{
  MainMenuSceneController mainMenuSceneController;

  private void Start()
  {
    mainMenuSceneController = FindObjectOfType<MainMenuSceneController>();
  }

  public void TransitionToMainControl()
  {
    mainMenuSceneController.TransitionToControl(MenuControlType.Main);
  }

  public void TransitionToPlayerSelectionControl()
  {
    mainMenuSceneController.TransitionToControl(MenuControlType.PlayerSelection);
  }

  public void TransitionToLevelSelectionControl()
  {
    mainMenuSceneController.TransitionToControl(MenuControlType.LevelSelection);
  }

  public void TransitionToCloudLevelControl()
  {
    mainMenuSceneController.TransitionToControl(MenuControlType.CloudLevels);
  }
}
