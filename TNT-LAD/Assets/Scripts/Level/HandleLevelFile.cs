using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class HandleLevelFile
{

  public char charDestructible { set; get; } = '+';
  public char charDefault { set; get; } = '*';
  public char charNone { set; get; } = '-';

  public void SaveMapFile(GameObject[,] blocks, string fileName)
  {
    CreateDir();
    OverwriteFile(blocks, fileName);
  }

  private void OverwriteFile(GameObject[,] blocks, string fileName)
  {
    DeleteFile(fileName);
    CreateFile(blocks, fileName);
  }

  //deletes the level file if it exists
  public void DeleteFile(string fileName)
  {
    if (File.Exists(GetFilePath(fileName)))
    {
      File.Delete(GetFilePath(fileName));
      File.Delete(GetFilePath(fileName) + ".meta");
    }
  }

  //Creates dir if it doesn't exist
  private void CreateDir()
  {
    if (!Directory.Exists(GetDirPath()))
    {
      Directory.CreateDirectory(GetDirPath());
    }
  }
  private void CreateFile(GameObject[,] blocks, string fileName)
  {
    var sr = File.CreateText(GetFilePath(fileName)); //creates file with scene name
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

  public string[] GetFileData(string fileName)
  {
    var sr = new StreamReader(GetFilePath(fileName));

    var fileContents = sr.ReadToEnd();
    sr.Close();
    string[] blockLine = fileContents.Split("\n"[0]);
    return blockLine;
  }

  public string GetFilePath(string fileName)
  {
    return Application.dataPath + "/Resources/LevelFiles/" + fileName + ".txt";
  }

  public static string GetDirPath()
  {
    return Application.dataPath + "/Resources/LevelFiles";
  }
}
