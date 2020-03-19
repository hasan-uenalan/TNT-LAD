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

  public IEnumerator RequestLevelDetails(int id, Action<string, string> callback = null)
  {
    string detailUrl = url + "GetCloudLevelDetails.php/get?levelId=" + id;
    var request = UnityWebRequest.Get(detailUrl);
    yield return request.SendWebRequest();

    var data = request.downloadHandler.text;

    ResponseCloudLevelDetails response = JsonUtility.FromJson<ResponseCloudLevelDetails>(data);
    if (response.success)
    {
      callback?.Invoke(response.levelName, response.previewImage);
    }
    else
    {
      callback?.Invoke(null, null);
    }
  }
}
