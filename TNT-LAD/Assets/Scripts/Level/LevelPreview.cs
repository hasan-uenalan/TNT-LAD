using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelPreview : MonoBehaviour
{
  public byte[] TakePreviewImage(int resWidth, int resHeight)
  {
    var cam = gameObject.GetComponent<Camera>();

    RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
    cam.targetTexture = rt;
    Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
    cam.Render();
    RenderTexture.active = rt;
    screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
    cam.targetTexture = null;
    RenderTexture.active = null;
    Destroy(rt);
    return screenShot.EncodeToPNG();
  }
}
