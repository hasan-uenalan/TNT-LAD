﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FetchLevels
{

  public List<string> LoadLevelNames()
  {
    List<string> levelNames;
    levelNames = new List<string>();
    if (Directory.Exists(HandleLevelFile.GetDirPath()))
    {
      DirectoryInfo dirInfo = new DirectoryInfo(HandleLevelFile.GetDirPath());
      FileInfo[] files = dirInfo.GetFiles("*.*");
      foreach (var file in files)
      {
        if (file.ToString().Contains(".meta"))
        {
          continue;
        }
        levelNames.Add(Path.GetFileNameWithoutExtension(file.ToString()));
      }
    }
    return levelNames;
  }
}