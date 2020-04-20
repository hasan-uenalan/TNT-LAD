using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicUIHandler : MonoBehaviour
{

  public GameObject MusicUISlider;

  private Slider musicSlider;

  private OptionsFileWriter optionsFileWriter;

  void Start()
  {
    optionsFileWriter = new OptionsFileWriter();
    musicSlider = MusicUISlider.GetComponent<Slider>();
    musicSlider.value = (CrossSceneInformation.MusicVolume*100);
  }

  public void SetVolume()
  {
    CrossSceneInformation.MusicVolume = (musicSlider.value/100);
    optionsFileWriter.WriteToOptionsFile();
  }
}
