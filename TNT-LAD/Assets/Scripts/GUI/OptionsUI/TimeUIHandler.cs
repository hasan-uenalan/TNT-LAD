using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TimeUIHandler : MonoBehaviour
{
  public GameObject TimeInputField;

  private InputField inputField;

  private int oldTimeValue;

  private OptionsFileWriter optionsFileWriter;


  // Start is called before the first frame update
  void Start()
  {
    optionsFileWriter = new OptionsFileWriter();
    inputField = TimeInputField.GetComponent<InputField>();
    inputField.text = CrossSceneInformation.RoundTime.ToString();
    oldTimeValue = CrossSceneInformation.RoundTime;
  }

  public void SetRoundTime()
  {
    if (System.Int32.Parse(TimeInputField.GetComponent<InputField>().text) >= 0)
    {
      CrossSceneInformation.RoundTime = System.Int32.Parse(inputField.text);
      optionsFileWriter.WriteToOptionsFile();
    }
    else 
    {
      TimeInputField.GetComponent<InputField>().text = oldTimeValue.ToString();
    }
  }
}
