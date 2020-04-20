using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
  public bool paused;

  private MusicHandler musicHandler;
  private double timeLeft;
  private void Start()
  {
    musicHandler = FindObjectOfType<MusicHandler>();
    timeLeft = CrossSceneInformation.RoundTime;
    gameObject.GetComponent<Text>().text = CrossSceneInformation.RoundTime.ToString();
  }

  // Update is called once per frame
  void Update()
  {
    if (paused)
    {
      return;
    }
    timeLeft -= Time.deltaTime;
    if (timeLeft >= 0) {
      gameObject.GetComponent<Text>().text = timeLeft.ToString("0");
    }
    else {
      if(gameObject.GetComponent<Text>().color != Color.red)
      {
        musicHandler.ChangeTrack("SuddonDeath");
      }
      gameObject.GetComponent<Text>().color = Color.red;
      gameObject.GetComponent<Text>().text = "Sudden Death";
    }
  }
}
