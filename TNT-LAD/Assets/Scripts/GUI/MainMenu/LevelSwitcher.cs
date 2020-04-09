using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelSwitcher : MonoBehaviour
{
  public Image previewImage;
  public Text levelNameText;

  private List<Object> LevelImageList;

  private int curLevelIndex = 0;

  private void Start()
  {
    LevelImageList = new List<Object>();
    DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Application.dataPath, "../LevelFiles"));
    foreach(var imageFile in dirInfo.GetFiles("*.png"))
    {
      LevelImageList.Add(LoadSprite(imageFile.FullName));
    }
    UpdateLevelSelectionValues();
  }

  private void OnEnable()
  {
    if(LevelImageList != null)
    {
      UpdateLevelSelectionValues();
    }
  }

  private void UpdateLevelSelectionValues()
  {
    CrossSceneInformation.currentLevel.SetLevel(LevelImageList[curLevelIndex].name);
    levelNameText.text = CrossSceneInformation.currentLevel.levelFileName;
    previewImage.sprite = (Sprite)LevelImageList[curLevelIndex];
  }

  public void LevelRightClick()
  {
    if (curLevelIndex < LevelImageList.Count - 1) {
      curLevelIndex++;
      UpdateLevelSelectionValues();

    }
    else {
      curLevelIndex = 0;
      UpdateLevelSelectionValues();
    }
  }

  public void LevelLeftClick()
  {
    if (curLevelIndex > 0) {
      curLevelIndex--;
      UpdateLevelSelectionValues();
    }
    else {
      curLevelIndex = LevelImageList.Count - 1;
      UpdateLevelSelectionValues();
    }
  }

  private Sprite LoadSprite(string path)
  {
    if (string.IsNullOrEmpty(path)) return null;
    if (File.Exists(path))
    {
      byte[] bytes = System.IO.File.ReadAllBytes(path);
      Texture2D texture = new Texture2D(1, 1);
      texture.LoadImage(bytes);
      Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
      sprite.name = Path.GetFileNameWithoutExtension(path);
      return sprite;
    }
    return null;
  }
}
