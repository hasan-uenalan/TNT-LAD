using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUIHandler : MonoBehaviour
{

  InputField inputField;

  // Start is called before the first frame update
  void Start()
  {
    inputField = gameObject.transform.GetChild(0).GetComponent<InputField>();
    inputField.text = CrossSceneInformation.RoundTime.ToString();
  }

  public void SetRoundTime()
  {
    CrossSceneInformation.RoundTime = System.Int32.Parse(inputField.text);
  }
}
