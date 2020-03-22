using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuSceneController : MonoBehaviour
{
  public List<MenuControl> mainMenuSceneControls;
  public GameObject mainCamera; 

  MenuControl currentControl; 

  void Start()
  {
    currentControl = GetControlFromType(MenuControlType.Main);
  }

  public void TransitionToControl(MenuControlType control)
  {
    currentControl = GetControlFromType(control);
    LeanTween.move(mainCamera, GetControlFromType(control).cameraPivot.transform.position, 0.8f).setEaseInOutSine();
    LeanTween.rotate(mainCamera, GetControlFromType(control).cameraPivot.transform.rotation.eulerAngles, 0.8f).setEaseInOutSine();
  }


  private MenuControl GetControlFromType(MenuControlType controlType)
  {
    return mainMenuSceneControls.FirstOrDefault(x => x.controlType == controlType);
  }
}

[Serializable]
public struct MenuControl{
  public MenuControlType controlType;
  public GameObject controlObject;
  public GameObject cameraPivot;
}

public enum MenuControlType
{
  Main,
  PlayerSelection,
  LevelSelection,
  CloudLevels
}