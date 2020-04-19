using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUIHandler : MonoBehaviour
{
  public GameObject LifeText;
  private int currentLifes;
  private OptionsFileWriter optionsFileWriter;

  // Start is called before the first frame update
  void Start()
  {
    currentLifes = CrossSceneInformation.PlayerLifes;
    LifeText.GetComponent<Text>().text = currentLifes.ToString();
    optionsFileWriter = new OptionsFileWriter();
  }

  private void SetNewLifes()
  {
    LifeText.GetComponent<Text>().text = currentLifes.ToString();
    CrossSceneInformation.PlayerLifes = currentLifes;
    optionsFileWriter.WriteToOptionsFile();
  }

  public void SubtractLife()
  {
    if (currentLifes > 1)
    {
      currentLifes--;
      SetNewLifes();
    }
  }

  public void AddLife()
  {
    if (currentLifes <= 3)
    {
      currentLifes++;
      SetNewLifes();
    }
  }
}
