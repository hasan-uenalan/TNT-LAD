using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FetchLevels : MonoBehaviour
{
  private List<string> levelNames;

  //needed components
  HandleLevelFile handleLevelFile;

  private void Start()
  {
    handleLevelFile = gameObject.GetComponent<HandleLevelFile>();
    LoadLevelNames();
  }

  public List<string> LoadLevelNames()
  {
    levelNames = new List<string>();
    DirectoryInfo dirInfo = new DirectoryInfo(handleLevelFile.GetDirPath());
    FileInfo[] files = dirInfo.GetFiles("*.*");
    foreach (var file in files)
    {
      if (file.ToString().Contains(".meta"))
      {
        continue;
      }
      levelNames.Add(Path.GetFileNameWithoutExtension(file.ToString()));
    }
    return levelNames;
  }
}
