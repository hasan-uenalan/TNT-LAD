using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControlTransitioner : MonoBehaviour
{

  public GameObject PlayerInput;

  public GameObject CharacterHandler;

  public GameObject CloudHandler;

  private MainMenuSceneController mainMenuSceneController;

  private void Start()
  {
    mainMenuSceneController = FindObjectOfType<MainMenuSceneController>();
  }

  public void TransitionToMainControl()
  {
    PlayerInput.SetActive(false);
    mainMenuSceneController.TransitionToControl(MenuControlType.Main);
  }

  public void TransitionToPlayerSelectionControl()
  {
    PlayerInput.SetActive(true);
    mainMenuSceneController.TransitionToControl(MenuControlType.PlayerSelection);
  }

  public void TransitionToLevelSelectionControl()
  {
    PlayerInput.SetActive(false);
    mainMenuSceneController.TransitionToControl(MenuControlType.LevelSelection);
  }

  public void TransitionToCloudLevelControl()
  {
    mainMenuSceneController.TransitionToControl(MenuControlType.CloudLevels);
  }
}
