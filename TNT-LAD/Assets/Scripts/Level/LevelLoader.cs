using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Sets the parameters so the other components can load the level
public class LevelLoader : MonoBehaviour
{
  //components
  private LevelController levelController;

  void Start()
  {
    levelController = gameObject.GetComponent<LevelController>();
    LoadLevel();
  }

  private void LoadLevel()
  {
    if(CrossSceneInformation.currentLevel.levelFileName != null)
    {
      LoadLocalLevel();
    }
    else
    {
      LoadCloudLevel();
    }
  }

  private void LoadLocalLevel()
  {
    levelController.currentFile = CrossSceneInformation.currentLevel.levelFileName;
    levelController.ConstructByFile();
  }

  private void LoadCloudLevel()
  {
    levelController.currentFile = null;
    levelController.ConstructCloudLevel(CrossSceneInformation.currentLevel.cloudLevel);
  }
}
