using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class to save values between loading scenes
public class SceneLoader : MonoBehaviour
{

  static public string levelFileName;

  public void LoadLevel(string levelName)
  {
    levelFileName = levelName;
    //Level is the standard scene for loading levels
    SceneManager.LoadScene("Level");
  }

  //For test purposes
  public void LoadLevel()
  {
    levelFileName = "Level 1";
    //Level is the standard scene for loading levels
    SceneManager.LoadScene("Level");
  }


}
