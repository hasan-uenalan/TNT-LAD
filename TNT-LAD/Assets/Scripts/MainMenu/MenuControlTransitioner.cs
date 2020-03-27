using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControlTransitioner : MonoBehaviour
{

  public CharacterJoiner characterJoiner;
  public LevelSwitcher levelSwitcher;
  public CloudLevelSelectionHandler cloudLevelSelectionHandler;

  private MainMenuSceneController mainMenuSceneController;

  private void Start()
  {
    mainMenuSceneController = FindObjectOfType<MainMenuSceneController>();
  }

  public void TransitionToMainControl()
  {
    mainMenuSceneController.TransitionToControl(MenuControlType.Main);
    SetActiveControlScript(null);
  }

  public void TransitionToPlayerSelectionControl()
  {
    mainMenuSceneController.TransitionToControl(MenuControlType.PlayerSelection);
    SetActiveControlScript(typeof(CharacterJoiner));
  }

  public void TransitionToLevelSelectionControl()
  {
    mainMenuSceneController.TransitionToControl(MenuControlType.LevelSelection);
    SetActiveControlScript(typeof(LevelSwitcher));
  }

  public void TransitionToCloudLevelControl()
  {
    mainMenuSceneController.TransitionToControl(MenuControlType.CloudLevels);
    SetActiveControlScript(typeof(CloudLevelSelectionHandler));
  }

  private void SetActiveControlScript(Type type)
  {
    characterJoiner.enabled = type == characterJoiner.GetType();
    levelSwitcher.enabled = type == levelSwitcher.GetType();
    cloudLevelSelectionHandler.enabled = type == cloudLevelSelectionHandler.GetType();
  }
}
