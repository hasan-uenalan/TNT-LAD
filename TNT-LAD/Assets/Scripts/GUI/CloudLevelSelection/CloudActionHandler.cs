using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CloudActionHandler
{
  private readonly string url = "http://tnt-lad.000webhostapp.com/phpscripts/";

  public IEnumerator RequestLevelIds(Action<List<int>> callback = null)
  {
    string idUrl = url + "GetCloudLevelIds.php";
    var request = UnityWebRequest.Get(idUrl);
    yield return request.SendWebRequest();

    var data = request.downloadHandler.text;

    ResponseCloudLevelIds response = JsonUtility.FromJson<ResponseCloudLevelIds>(data);
    if (response.success)
    {
      callback?.Invoke(response.levelIds);
    }
    else
    {
      callback?.Invoke(null);
    }
  }
}
