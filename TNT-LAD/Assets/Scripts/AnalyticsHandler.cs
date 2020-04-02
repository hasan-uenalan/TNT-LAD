using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsHandler : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    Analytics.CustomEvent("gameStarted");
  }

  private void lol()
  {
    //Analytics.CustomEvent("lol", new Dictionary<string, Vector3>
    //{
    //  { },
    //  { }
    //});
  }
  
  // Update is called once per frame
  void Update()
  {

  }
}
