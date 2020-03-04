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

  public void SaveMapFile(GameObject[,] blocks)
  {
    CreateDir();
    DeleteFile();
    OverwriteFile(blocks);
  }
  //Creates dir if it doesn't exist
  private void CreateDir()
  {
    if (!Directory.Exists(GetDirPath()))
    {
      Directory.CreateDirectory(GetDirPath());
    }
  }

  public string[] GetFileData()
  {
    var sr = new StreamReader(GetFilePath());

    var fileContents = sr.ReadToEnd();
    sr.Close();
    string[] blockLine = fileContents.Split("\n"[0]);
    return blockLine;
  }

  private void OverwriteFile(GameObject[,] blocks)
  {
    DeleteFile();
    CreateFile(blocks);
  }

  //deletes the level file if it exists
  public void DeleteFile()
  {
    if (File.Exists(GetFilePath()))
    {
      File.Delete(GetFilePath());
      File.Delete(GetFilePath() + ".meta");
    }
  }

  public void CreateFile(GameObject[,] blocks)
  {
    var sr = File.CreateText(GetFilePath()); //creates file with scene name
    for (int x = 0; x < blocks.GetLength(0); x++)
    {
      for (int z = 0; z < blocks.GetLength(1); z++)
      {
        if (blocks[x, z] == null)
        {
          sr.Write(charNone);
        }
        else
        {
          if (blocks[x, z].gameObject.GetComponent<BlockData>().GetBlockType() == BlockData.BlockType.DESTRUCTIBLE)
          {
            sr.Write(charDestructible);
          }
          if (blocks[x, z].gameObject.GetComponent<BlockData>().GetBlockType() == BlockData.BlockType.DEFAULT)
          {
            sr.Write(charDefault);
          }
        }

      }
      if (x < blocks.GetLength(0) - 1) //not starting a new line at the end of document
      {
        sr.Write("\n");
      }
    }
      sr.Close();
  }
  //TODO: Delete those
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

  public string GetFilePath()
  {
    return Application.dataPath + "/Resources/LevelFiles/" + SceneManager.GetActiveScene().name + ".txt";
  }

  public string GetDirPath()
  {
    return Application.dataPath + "Ressources/LevelFiles";
  }

}
