using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FetchLevels
{

  public List<string> LoadLevelNames(string directory)
  {
    List<string> levelNames;
    levelNames = new List<string>();
    if (Directory.Exists(directory))
    {
      DirectoryInfo dirInfo = new DirectoryInfo(directory);
      FileInfo[] files = dirInfo.GetFiles("*.txt");
      foreach (var file in files)
      {
        levelNames.Add(Path.GetFileNameWithoutExtension(file.ToString()));
      }
    }
    return levelNames;
  }
}
