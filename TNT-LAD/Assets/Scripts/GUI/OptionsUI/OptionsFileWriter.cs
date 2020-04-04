using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OptionsFileWriter
{
  public int RoundTime;

  public int PlayerLifes;

  private readonly string fullFileName;

  private readonly OptionsData optionsData;

  public OptionsFileWriter()
  {
    fullFileName = Path.Combine(Application.dataPath, "Resources/Options.txt");
    optionsData = new OptionsData();
  }

  public void WriteToOptionsFile()
  {
    optionsData.RoundTime = CrossSceneInformation.RoundTime;
    optionsData.PlayerLifes = CrossSceneInformation.PlayerLifes;
    string optionsJson = JsonUtility.ToJson(optionsData);
    File.WriteAllText(fullFileName, optionsJson);
  }
}
