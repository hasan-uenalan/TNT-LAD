using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// is only called at the start of the Application!
/// </summary>
public class FileLoader : MonoBehaviour
{
  private static string directory;

  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
  static void SaveValuesFromOptionsFile()
  {
    directory = Path.Combine(Application.dataPath, "Resources");
    TextAsset optionsFile = (TextAsset) Resources.Load("Options");
    if (optionsFile == null)
    {
      CreateNewOptionsFile();
      return;
    }
    SetOptionsFromFile(optionsFile);
  }

  private static void CreateNewOptionsFile()
  {
    OptionsData optionsData = new OptionsData();
    string optionsJson = JsonUtility.ToJson(optionsData);
    File.WriteAllText(Path.Combine(directory, "Options.txt"), optionsJson);
    SetOptionsFromOptionsData(optionsData);
  }

  private static void SetOptionsFromOptionsData(OptionsData optionsData)
  {
    CrossSceneInformation.PlayerLifes = optionsData.PlayerLifes;
    CrossSceneInformation.RoundTime = optionsData.RoundTime;
  }

  private static void SetOptionsFromFile(TextAsset optionsFile)
  {
    OptionsData optionsData = OptionsData.CreateOptionsData(optionsFile);
    SetOptionsFromOptionsData(optionsData);
  }
}
