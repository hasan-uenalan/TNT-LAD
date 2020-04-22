using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUIHandler : MonoBehaviour
{
  public GameObject SoundUISlider;

  private Slider soundSlider;

  private OptionsFileWriter optionsFileWriter;

  void Start()
  {
    optionsFileWriter = new OptionsFileWriter();
    soundSlider = SoundUISlider.GetComponent<Slider>();
    soundSlider.value = (CrossSceneInformation.SoundVolume * 100);
  }

  public void SetVolume()
  {
    CrossSceneInformation.SoundVolume = (soundSlider.value / 100);
    optionsFileWriter.WriteToOptionsFile();
  }
}
