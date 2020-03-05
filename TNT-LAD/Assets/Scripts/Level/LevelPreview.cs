using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelPreview : MonoBehaviour
{
  public void TakePreviewImage(Camera camera, int resWidth, int resHeight, string targetPath)
  {
    RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
    camera.targetTexture = rt;
    Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
    camera.Render();
    RenderTexture.active = rt;
    screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
    camera.targetTexture = null;
    RenderTexture.active = null;
    Destroy(rt);
    byte[] bytes = screenShot.EncodeToPNG();
    Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
    File.WriteAllBytes(targetPath, bytes);
    Debug.Log($"PreviewImage written to: {targetPath}");
  }
}
