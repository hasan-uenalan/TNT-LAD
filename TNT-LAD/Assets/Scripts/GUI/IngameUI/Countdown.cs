using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
  private double timeLeft;
  private void Start()
  {
    timeLeft = CrossSceneInformation.RoundTime;
    gameObject.GetComponent<Text>().text = CrossSceneInformation.RoundTime.ToString();
  }

  // Update is called once per frame
  void Update()
  {
    timeLeft -= Time.deltaTime;
    if (timeLeft >= 0) {
      gameObject.GetComponent<Text>().text = timeLeft.ToString("0");
    }
    else {
      gameObject.GetComponent<Text>().color = Color.red;
      gameObject.GetComponent<Text>().text = "Sudden Death";
    }
  }
}
