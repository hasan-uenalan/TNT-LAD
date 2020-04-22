using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class OptionsData
{
  public int RoundTime;

  public int PlayerLifes;

  public float MusicVolume;
  
  public float SoundVolume;

  public OptionsData()
  {
    RoundTime = 300;
    PlayerLifes = 3;
    MusicVolume = 0.75f;
    SoundVolume = 0.75f;
  }

  public static OptionsData CreateOptionsData(TextAsset optionsFile)
  {
    return JsonUtility.FromJson<OptionsData>(optionsFile.text);
  }
}
