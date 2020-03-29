using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsTracker : MonoBehaviour
{
  // Start is called before the first frame update

  private int test = 0;

  void Start()
  {
    AnlGameStarted();
  }
  
  private void AnlGameStarted()
  {
    Analytics.EnableCustomEvent("gameStarted", true);
    Analytics.CustomEvent("gameStarted", Vector3.zero);
    Debug.Log("Custom Event started");
  }

}
