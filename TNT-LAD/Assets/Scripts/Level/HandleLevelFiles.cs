using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class HandleLevelFiles
{

  public char charDestructible { set; get; } = '+';
  public char charDefault { set; get; } = '*';
  public char charNone { set; get; } = '-';

  public readonly string directory; 

  public HandleLevelFiles()
  {
    directory = Path.Combine(Application.dataPath, "Resources/LevelFiles");
  }

  public void SaveLevelFiles(GameObject[,] blocks, byte[] previewImage, string fileName)
  {
    Directory.CreateDirectory(directory);
    OverwriteLevelFiles(blocks, previewImage, fileName);
  }

  private void OverwriteLevelFiles(GameObject[,] blocks, byte[] previewImage, string fileName)
  {
    DeleteLevelFile(fileName);
    CreateLevelTxt(blocks, fileName);
    CreateLevelImage(previewImage, fileName);
  }

  private void CreateLevelImage(byte[] previewImage, string fileName)
  {
    File.WriteAllBytes(GetFilePath(fileName, ".png"), previewImage);
  }

  //deletes the level files if it exists
  public void DeleteLevelFile(string fileName)
  {
    if (File.Exists(GetFilePath(fileName, ".txt")))
    {
      File.Delete(GetFilePath(fileName, ".txt"));
      File.Delete(GetFilePath(fileName, ".txt") + ".meta");
    }
    if (File.Exists(GetFilePath(fileName, ".png")))
    {
      File.Delete(GetFilePath(fileName, ".png"));
      File.Delete(GetFilePath(fileName, ".png") + ".meta");
    }
  }

  private void CreateLevelTxt(GameObject[,] blocks, string fileName)
  {
    var sr = File.CreateText(GetFilePath(fileName, ".txt")); //creates file with scene name
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
    var sr = new StreamReader(GetFilePath(fileName, ".txt"));

    var fileContents = sr.ReadToEnd();
    sr.Close();
    string[] blockLine = fileContents.Split("\n"[0]);
    for(int i=0; i<blockLine.Length; i++)
    {
      blockLine[i] = blockLine[i].Trim();
    }
    return blockLine;
  }

  public string GetFilePath(string fileName, string extension)
  {
    return Path.Combine(directory, fileName + extension);
  }
}
