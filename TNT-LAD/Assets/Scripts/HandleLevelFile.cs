using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class HandleLevelFile : MonoBehaviour
{

  private char charDestructible = '+';
  private char charDefault = '*';
  private char charNone = '-';

  public string[] GetFileData()
  {
    var sr = new StreamReader(GetFilePath());

    var fileContents = sr.ReadToEnd();
    sr.Close();
    string[] blockLine = fileContents.Split("\n"[0]);
    return blockLine;
  }

  public string GetFilePath()
  {
    return Application.dataPath + "/Ressources/LevelFiles/" + SceneManager.GetActiveScene().name + ".txt";
  }

  public void saveMapFile(GameObject[,] blocks)
  {
    if (File.Exists(GetFilePath()))
    {
      File.Delete(GetFilePath());
    }
    else
    {
      Debug.Log("Test");
      var sr = File.CreateText(Application.dataPath + "/Ressources/LevelFiles/" + SceneManager.GetActiveScene().name); //creates file with scene name
      for(int x = 0; x < blocks.GetLength(0); x++)
      {
        for(int z = 0; z < blocks.GetLength(1); z++)
        {

        }
      }
    }
  }

  public char GetCharDestructible() 
  {
    return charDestructible;    
  }
  public char GetCharDefault()
  {
    return charDefault;
  }
  public char GetCharNone()
  {
    return charNone;
  }

}
