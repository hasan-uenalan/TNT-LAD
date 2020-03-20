using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CloudLevelSelectionHandler : MonoBehaviour
{
  public CloudActionHandler cloudActionHandler;
  public List<CloudLevel> cloudLevels = new List<CloudLevel>();
  public Text pageLabelText;

  public GameObject levelCardPrefab;
  public GameObject[] levelCardSlots = new GameObject[3];

  private int currentPageIndex = 0;
  private int levelPages;

  void Start()
  {
    cloudActionHandler = new CloudActionHandler();
    StartCoroutine(cloudActionHandler.RequestLevelIds(SetLevelIdsCallback)); //Start request 
  }

  /// <summary>
  /// Callback handler after request for level ids is complete. Starts setup of cloud level selection.
  /// </summary>
  /// <param name="levelIds"></param>
  private void SetLevelIdsCallback(List<int> levelIds)
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
    int mod = cloudLevels.Count % levelCardSlots.Length;
    int raw = (cloudLevels.Count / levelCardSlots.Length);
    levelPages = (mod == 0) ? raw : raw + 1;
    UpdatePageLabel();
    UpdateLevelCards();
  }

  public void PageRight()
  {
    if(currentPageIndex < levelPages - 1)
    {
      currentPageIndex++;
      UpdatePageLabel();
      UpdateLevelCards();
    }
  }

  public void PageLeft()
  {
    if (currentPageIndex > 0)
    {
      currentPageIndex--;
      UpdatePageLabel();
      UpdateLevelCards();
    }
  }

  public void SelectLevelCard(GameObject guiCard)
  {
    CloudLevel selectedCloudLevel = cloudLevels.FirstOrDefault(x => x.guiCard == guiCard);
    if (string.IsNullOrEmpty(selectedCloudLevel.levelContent))
    {
      return; //content not yet loaded, selection shouldn't work
    }

    if (CrossSceneInformation.currentLevel.cloudLevel != null)
    {
      UpdateSelectionInGUI(false, CrossSceneInformation.currentLevel.cloudLevel.guiCard);
    }
    CrossSceneInformation.currentLevel.SetLevel(selectedCloudLevel);
    UpdateSelectionInGUI(true, CrossSceneInformation.currentLevel.cloudLevel.guiCard);
  }

  private void UpdateSelectionInGUI(bool select, GameObject levelCardToSelect)
  {
    levelCardToSelect.transform.GetChild(0).gameObject.SetActive(select);
  }

  private void UpdatePageLabel()
  {
    pageLabelText.text = $"{currentPageIndex + 1}/{levelPages}";
  }

  /// <summary>
  /// Updates all cloud level card slots and content.
  /// </summary>
  private void UpdateLevelCards()
  {
    List<CloudLevel> cloudLevelsToDisplay;
    int mod = cloudLevels.Count % levelCardSlots.Length;
    int lastPageRange = (mod == 0) ? levelCardSlots.Length : mod;
    int range = (currentPageIndex == levelPages - 1) ? lastPageRange : levelCardSlots.Length;
    int displayIndex = currentPageIndex * levelCardSlots.Length;
    cloudLevelsToDisplay = cloudLevels.GetRange(displayIndex, range);
    AssignLevelCardContents(cloudLevelsToDisplay);
  }

  /// <summary>
  /// Displays cloud levels on current page
  /// </summary>
  /// <param name="levelsToDisplay">cloud levels that need to be displayed on current page. If this list has more entries than slots are available the rest is ignored</param>

  private void AssignLevelCardContents(List<CloudLevel> levelsToDisplay)
  {
    for(int i = 0; i < levelCardSlots.Length; i++)
    {
      if(i > levelsToDisplay.Count - 1)
      {
        PlaceLevelCardGameObject(levelCardSlots[i]);
        continue;
      }

      var currCloudLevel = levelsToDisplay[i];
      PlaceLevelCardGameObject(levelCardSlots[i], currCloudLevel);

      if (!currCloudLevel.isInitialized)
      {
        StartCoroutine(cloudActionHandler.RequestLevelDetails(currCloudLevel.levelId, currCloudLevel.Initialize));
        currCloudLevel.isInitialized = true; //HACK if request fails still initialized, fix pls
      }
    }
  }

  /// <summary>
  /// Instantiates level card for a cloud level in the specified slot
  /// </summary>
  /// <param name="slot">Target slot for the level card</param>
  /// <param name="cloudLevel">cloud level which's guiCard should be displayed. Leave empty to delete card in <paramref name="slot"/></param>
  private void PlaceLevelCardGameObject(GameObject slot, CloudLevel cloudLevel = null)
  {
    GameObject currCard = GetActiveLevelCard(slot);
    if(currCard != null)
    {
      currCard.SetActive(false);
    }
    if(cloudLevel == null)
    {
      return; //only delete card, cancel rest
    }

    if(cloudLevel.guiCard == null)
    {
      var newCard = Instantiate(levelCardPrefab, slot.transform.position, slot.transform.rotation, slot.transform);
      cloudLevel.guiCard = newCard;
    }
    else
    {
      cloudLevel.guiCard.SetActive(true);
    }
  }

  private GameObject GetActiveLevelCard(GameObject slot)
  {
    foreach(Transform child in slot.transform)
    {
      if (child.gameObject.activeSelf)
      {
        return child.gameObject;
      }
    }
    return null;
  }
}
