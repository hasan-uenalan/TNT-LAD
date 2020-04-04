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
    directory = Path.Combine(Application.dataPath, "Resources/LevelFiles");
    TextAsset optionsFile = (TextAsset) Resources.Load("Options");
    if (optionsFile == null)
    {
      CreateNewOptionsFile();
    }
    SetOptionsFromFile(optionsFile);
  }

  private static void CreateNewOptionsFile()
  {
    string optionsJson = JsonUtility.ToJson(new OptionsData());
    File.WriteAllText(Path.Combine(directory, "Options.txt"), optionsJson);
  }

  private static void SetOptionsFromFile(TextAsset optionsFile)
  {
    OptionsData optionsData = OptionsData.CreateOptionsData(optionsFile);
    CrossSceneInformation.PlayerLifes = optionsData.PlayerLifes;
    CrossSceneInformation.RoundTime = optionsData.RoundTime;
  }
}
