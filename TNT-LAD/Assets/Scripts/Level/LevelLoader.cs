using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Sets the parameters so the other components can load the level
public class LevelLoader : MonoBehaviour
{
  private string levelFileName;

  //components
  private LevelController levelController;

  void Start()
  {
    levelFileName = SceneLoader.levelFileName;

    levelController = gameObject.GetComponent<LevelController>();

    levelController.currentFile = levelFileName;
    levelController.ConstructByFile();
  }

}
