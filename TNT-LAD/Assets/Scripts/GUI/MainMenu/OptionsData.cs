using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class OptionsData
{
  public int RoundTime{ get; private set; }

  public int PlayerLifes { get; private set; }

  public OptionsData()
  {
    RoundTime = 300;
    PlayerLifes = 3;
  }

  public static OptionsData CreateOptionsData(TextAsset optionsFile)
  {
    return JsonUtility.FromJson<OptionsData>(optionsFile.text);
  }
}
