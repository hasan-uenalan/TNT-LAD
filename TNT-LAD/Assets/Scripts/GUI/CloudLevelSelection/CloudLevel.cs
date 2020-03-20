using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudLevel
{
  public bool isInitialized { get; set; } = false;
  public int levelId  { get; set; }
  public string levelName { get; set; }
  public Sprite previewImage { get; set; }
  public string levelContent { get; set; }
  public GameObject guiCard { get; set; }

  public CloudLevel(int levelId) 
  {
    this.levelId = levelId;
  }

  public void Initialize(string levelName, string previewImageBaseString, string levelContent)
  {
    this.levelName = levelName;
    SetPreviewImage(previewImageBaseString);

    guiCard.transform.GetChild(2).GetComponent<Text>().text = levelName;
    var image = guiCard.transform.GetChild(3).GetComponent<Image>();
    image.enabled = true;
    image.sprite = previewImage;

    this.levelContent = levelContent;
  }

  private void SetPreviewImage(string imageString)
  {
    var texture = new Texture2D(128, 128, TextureFormat.RGBA32, false);
    var bytes = Convert.FromBase64String(imageString);
    texture.LoadImage(bytes);
    texture.Apply();
    
    var sprite = Sprite.Create(
      texture,
      new Rect(0, 0, texture.width, texture.height),
      new Vector2(texture.width / 2, texture.height / 2)
    );
    previewImage = sprite;
  }
}
