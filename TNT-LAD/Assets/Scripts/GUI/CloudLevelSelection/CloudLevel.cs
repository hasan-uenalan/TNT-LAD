using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLevel
{
  public bool isInitialized { get; set; } = false;
  public int levelId  { get; set; }
  public string levelName { get; set; }
  public Texture2D previewImage { get; set; }
  public GameObject guiCard { get; set; }

  public CloudLevel(int levelId) 
  {
    this.levelId = levelId;
  }

  public void Initialize(string levelName, Texture2D previewImage)
  {
    this.levelName = levelName;
    this.previewImage = previewImage;
    isInitialized = true;
  }
}
