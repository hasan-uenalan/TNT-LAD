using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

  private float timeLeft = 300.0f;

  private void Start()
  {
    gameObject.GetComponent<Text>().text = timeLeft.ToString("0");
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
      gameObject.GetComponent<Text>().text = (Time.deltaTime - 300.0f).ToString("0");
    }
  }
}
