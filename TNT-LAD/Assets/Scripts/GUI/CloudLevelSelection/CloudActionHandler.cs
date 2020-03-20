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
    var request = UnityWebRequest.Post(idUrl, new WWWForm());
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

  public IEnumerator RequestLevelDetails(int id, Action<string, string, string> callback = null)
  {
    WWWForm form = new WWWForm();
    form.AddField("levelId", id.ToString());
    string detailUrl = url + "GetCloudLevelDetails.php";
    var request = UnityWebRequest.Post(detailUrl,form);
    yield return request.SendWebRequest();

    var data = request.downloadHandler.text;

    ResponseCloudLevelDetails response = JsonUtility.FromJson<ResponseCloudLevelDetails>(data);
    if (response.success)
    {
      callback?.Invoke(response.levelName, response.previewImage, response.levelContent);
    }
    else
    {
      callback?.Invoke(null, null, null);
    }
  }

  public IEnumerator UploadLevelToCloud(string levelName, string previewImage, string levelContent, Action<bool> callback = null)
  {
    WWWForm form = new WWWForm();
    form.AddField("levelName", levelName);
    form.AddField("previewImage", previewImage);
    form.AddField("levelContent", levelContent);
    string detailUrl = url + "UploadLevelToCloud.php";
    var request = UnityWebRequest.Post(detailUrl,form);
    yield return request.SendWebRequest();

    var data = request.downloadHandler.text;

    ResponseCloudLevelDetails response = JsonUtility.FromJson<ResponseCloudLevelDetails>(data);
    if (response.success)
    {
      Debug.Log("Upload finished");
      callback?.Invoke(true);
    }
    else
    {
      callback?.Invoke(false);
    }
  }

}
