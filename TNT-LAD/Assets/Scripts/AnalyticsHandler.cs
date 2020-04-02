using UnityEngine.Analytics;

public static class AnalyticsHandler
{
  static void Start()
  {
    Analytics.CustomEvent("gameStarted");
  }

  private static void lol()
  {
    //Analytics.CustomEvent("lol", new Dictionary<string, Vector3>
    //{
    //  { },
    //  { }
    //});
  }
}
