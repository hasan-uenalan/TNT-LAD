using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControlTransitioner : MonoBehaviour
{

  public CharacterJoiner characterJoiner;

  private MainMenuSceneController mainMenuSceneController;

  private void Start()
  {
    mainMenuSceneController = FindObjectOfType<MainMenuSceneController>();
  }

  public void TransitionToMainControl()
  {
    characterJoiner.enabled = false;
    mainMenuSceneController.TransitionToControl(MenuControlType.Main);
  }

  public void TransitionToPlayerSelectionControl()
  {
    characterJoiner.enabled = true;
    mainMenuSceneController.TransitionToControl(MenuControlType.PlayerSelection);
  }

  public void TransitionToLevelSelectionControl()
  {
    characterJoiner.enabled = false;
    mainMenuSceneController.TransitionToControl(MenuControlType.LevelSelection);
  }

  public void TransitionToCloudLevelControl()
  {
    mainMenuSceneController.TransitionToControl(MenuControlType.CloudLevels);
  }
}
