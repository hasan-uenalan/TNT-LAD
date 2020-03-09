using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelSwitcher : MonoBehaviour
{
  public GameObject LevelInformation;

  private List<Object> LevelImageList;

  private int curLevelNumber = 1;

  private void Start()
  {
    LevelImageList = new List<Object>();
    LevelImageList = Resources.LoadAll("GUIComponents/LevelImages", typeof(Sprite)).ToList();
    GUIValues.LevelNumber = curLevelNumber;
  }

  private void ChangeLevelSelectionValues()
  {
    GUIValues.LevelNumber = curLevelNumber;
    string levelName = "Level " + curLevelNumber;
    GameObject.FindGameObjectWithTag("GUILevelButton").GetComponentInChildren<Text>().text = levelName;
    foreach (Sprite curLevelImage in LevelImageList) {
      if (curLevelImage.name.Equals(levelName)) {
        GameObject.FindGameObjectWithTag("GUILevelPreview").GetComponent<Image>().sprite = curLevelImage;
      }
    }
  }

  public void LevelRightClick()
  {
    if (curLevelNumber < LevelImageList.Count) {
      curLevelNumber++;
      ChangeLevelSelectionValues();

    }
    else {
      curLevelNumber = 1;
      ChangeLevelSelectionValues();
    }
  }

  public void LevelLeftClick()
  {
    if (curLevelNumber > 1) {
      curLevelNumber--;
      ChangeLevelSelectionValues();
    }
    else {
      curLevelNumber = LevelImageList.Count;
      ChangeLevelSelectionValues();
    }
  }
}
