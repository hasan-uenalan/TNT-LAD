using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelSwitcher : MonoBehaviour
{
  private List<Object> LevelImageList;

  private int curLevelNumber = 1;

  private void Start()
  {
    LevelImageList = new List<Object>();
    LevelImageList = Resources.LoadAll("Levels", typeof(Sprite)).ToList();
  }

  private void ChangeButtonName()
  {
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
      ChangeButtonName();

    }
    else {
      curLevelNumber = 1;
      ChangeButtonName();
    }
  }

  public void LevelLeftClick()
  {
    if (curLevelNumber > 1) {
      curLevelNumber--;
      ChangeButtonName();
    }
    else {
      curLevelNumber = LevelImageList.Count;
      ChangeButtonName();
    }
  }
}
