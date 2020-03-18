using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CloudLevelSelectionHandler : MonoBehaviour
{
  public CloudActionHandler cloudActionHandler;
  public List<CloudLevel> cloudLevels;
  public Text pageLabelText;

  public GameObject[] levelCardSlots = new GameObject[3];

  private int currentPageIndex = 0;
  private int levelPages;

  void Start()
  {
    cloudActionHandler = new CloudActionHandler();
    StartCoroutine(cloudActionHandler.RequestLevelIds(SetLevelIds));
  }

  private void SetLevelIds(List<int> levelIds)
  {
    if(levelIds == null)
    {
      Debug.LogWarning("SERVER ERROR: Couldn't fetch levels...");
      return;
    }

    foreach(int levelId in levelIds)
    {
      cloudLevels.Add(new CloudLevel(levelId));
    }
    SetupLevelPages();
  }

  private void SetupLevelPages()
  {
    levelPages = (cloudLevels.Count / 3) + 1;
    Debug.Log(levelPages);
    UpdatePageLabel();
  }

  public void PageRight()
  {
    if(currentPageIndex < levelPages - 1)
    {
      currentPageIndex++;
      UpdatePageLabel();
    }
  }

  public void PageLeft()
  {
    if (currentPageIndex > 0)
    {
      currentPageIndex--;
      UpdatePageLabel();
    }
  }

  private void UpdatePageLabel()
  {
    pageLabelText.text = $"{currentPageIndex + 1}/{levelPages}";
  }

  private void UpdateLevelCards()
  {
    var cloudLevelsToDisplay = new List<CloudLevel>();
    if (currentPageIndex == levelPages - 1)
    {
      var occupiedSlots = cloudLevels.Count % levelCardSlots.Length;
      cloudLevelsToDisplay = cloudLevels.Skip(Mathf.Max(0, cloudLevels.Count - occupiedSlots)).ToList();
    }
    //TODO: implement not only last page
    throw new System.NotImplementedException();
  }

  private void AssignLevelCardContents(List<CloudLevel> levelsToDisplay)
  {
    for(int i = 0; i < levelCardSlots.Length; i++)
    {
      if(i > levelsToDisplay.Count - 1)
      {
        //delete cloud level card
      }
      //assign cloudlevel gameobject to slot
    }
  }
}
