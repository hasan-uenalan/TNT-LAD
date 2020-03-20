using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class LevelEditor : MonoBehaviour
{

  //For editor
  public bool placeBlocksActive = true;
  private BlockData.BlockType activeBlockType = BlockData.BlockType.DESTRUCTIBLE;
  public ScenesSwitcher sceneSwitcher;

  //necessary components
  private LevelController levelController;
  private HandleLevelFiles handleLevelFile;
  private FetchLevels fetchLevels;

  //Pos for mouse pointer
  private int posX;
  private int posZ;

  //camera for level preview image
  public Camera previewCam;

  void Start()
  {
    handleLevelFile = new HandleLevelFiles();
    fetchLevels = new FetchLevels();

    levelController = gameObject.GetComponent<LevelController>();

    SelectDestructibleBlock();

    UIButtonLoadLevelInteractable(false);

    UIDropdownAddListener();
    LoadUIValues();
  }

  // Update is called once per frame
  void Update()
  {
    SelectObjectMouse();
  }

  private void SelectObjectMouse()
  {
    if (Input.GetMouseButtonDown(0) && placeBlocksActive && !IsPointerOverUIElement())
    {
      Vector2 pos = GetMousePositions();
      posX = Mathf.RoundToInt(pos.x);
      posZ = Mathf.RoundToInt(pos.y);
      if (!levelController.OutOfBoundsCheck(posX, posZ))
      {
        levelController.SetBlock(posX, posZ, activeBlockType);
      }
    }
    if (Input.GetMouseButtonDown(2) && placeBlocksActive && !IsPointerOverUIElement())
    {
      Vector2 pos = GetMousePositions();
      posX = Mathf.RoundToInt(pos.x);
      posZ = Mathf.RoundToInt(pos.y);
      if (!levelController.OutOfBoundsCheck(posX, posZ))
      {
        levelController.DeleteBlock(posX, posZ);
      }
    }
  }

  private Vector2 GetMousePositions()
  {
    Vector2 pos = new Vector2(levelController.gridX + 1, levelController.gridZ + 1);

    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, 100))
    {
      pos = new Vector2(hit.transform.gameObject.transform.position.x, hit.transform.gameObject.transform.position.z);
    }
    return pos;
  }

  public void UpdateMapSize()
  {
    InputField inputFieldX = GameObject.Find("InputFieldMapX").GetComponent<InputField>();
    InputField inputFieldZ = GameObject.Find("InputFieldMapZ").GetComponent<InputField>();

    int oldGridX = levelController.gridX;
    int oldGridZ = levelController.gridZ;

    int newGridX = int.Parse(inputFieldX.text);
    int newGridZ = int.Parse(inputFieldZ.text);

    if(newGridX <= 0 || newGridZ <= 0)
    {
      inputFieldX.text = oldGridX + "";
      inputFieldZ.text = oldGridZ + "";
      return;
    }

    levelController.gridX = newGridX;
    levelController.gridZ = newGridZ;

    levelController.Construct(); 
  }

  private string ReadInputFileName()
  {
    Regex lettersNumbers = new Regex("[A-Za-z0-9]*");
    string fileName;
    InputField newFileName = GameObject.Find("InputFieldLevelName").GetComponent<InputField>();

    fileName = lettersNumbers.Match(newFileName.text).ToString();
    return fileName;
  }

  #region UIStuff (should be eventually moved to own script)
  public void SelectDefaultBlock()
  {
    Button buttonDestructible = GameObject.Find("ButtonDestructible").GetComponent<Button>();
    Button buttonDefault = GameObject.Find("ButtonDefault").GetComponent<Button>();
    buttonDefault.interactable = false;
    buttonDestructible.interactable = true;
    activeBlockType = BlockData.BlockType.DEFAULT;
  }

  public void SelectDestructibleBlock()
  {
    Button buttonDestructible = GameObject.Find("ButtonDestructible").GetComponent<Button>();
    Button buttonDefault = GameObject.Find("ButtonDefault").GetComponent<Button>();
    buttonDefault.interactable = true;
    buttonDestructible.interactable = false;
    activeBlockType = BlockData.BlockType.DESTRUCTIBLE;
  }

  /// <summary>
  /// Checks if the layer of the clicked gameobject is on a UI layer
  /// </summary>
  /// <returns>true if gameobject is on UI layer</returns>
  public static bool IsPointerOverUIElement()
  {
    var eventData = new PointerEventData(EventSystem.current);
    eventData.position = Input.mousePosition;
    var results = new List<RaycastResult>();
    EventSystem.current.RaycastAll(eventData, results);
    return results.Count > 0;
  }

  private void UIDropdownAddListener()
  {
    Dropdown dropDownLevels = GameObject.Find("DropdownMapSelection").GetComponent<Dropdown>();
    dropDownLevels.onValueChanged.AddListener(delegate
    {
      UIDropdownOnValueChanged(dropDownLevels);
    });
  }

  private void UIDropdownOnValueChanged(Dropdown change)
  {
    if(GetSelectedLevel() != null)
    {
      LoadLevelFile();
    }
    if(GetSelectedLevel() == "-")
    {
      UIButtonLoadLevelInteractable(false);
    }
    else
    {
      UIButtonLoadLevelInteractable(true);
    }
  }

  public void UIButtonLoadLevelInteractable(bool value)
  {
    Button buttonLoadLevel = GameObject.Find("ButtonLoadLevel").GetComponent<Button>();
    buttonLoadLevel.interactable = value;
  }

  //Reads name of map, saves file and updates currently loaded map
  public void SaveFile()
  {
    previewCam.GetComponent<CameraPositioning>().CenterCameraPosition();
    var imageBytes = previewCam.GetComponent<LevelPreview>().TakePreviewImage( 512, 512 );
    handleLevelFile.SaveLevelFiles(levelController.blockMap, imageBytes, ReadInputFileName());
    LoadUIValues();
    LoadUIDropdownSetByFileName(ReadInputFileName());
  }

  public void UpdateCurrentFile()
  {
    LoadUIValues();
  }

  public void LoadLevel()
  {
    CrossSceneInformation.currentLevel.SetLevel(GetSelectedLevel());
    sceneSwitcher.SwitchToGame();
  }

  public void DeleteFile()
  {
    handleLevelFile.DeleteLevelFile(GetSelectedLevel());
    LoadUIValues();
    LoadUIDropdownFirstLevel();
  }

  public void LoadLevelFile()
  {
    Dropdown dropDownLevels = GameObject.Find("DropdownMapSelection").GetComponent<Dropdown>();
    InputField newFileName = GameObject.Find("InputFieldLevelName").GetComponent<InputField>();
    if(dropDownLevels.value == 0)
    {
      levelController.currentFile = null;
      levelController.Construct();
      newFileName.text = "";
    }
    else
    {
      levelController.currentFile = GetSelectedLevel();
      levelController.ConstructByFile();
      newFileName.text = GetSelectedLevel();
    }

    LoadUIGridSize();
  }

  //updates UI Values so they adapt to change of map
  private void LoadUIValues()
  {
    LoadUIGridSize();
    LoadUIDropdownOptions();
  }

  private void LoadUIGridSize()
  {
    InputField inputFieldX = GameObject.Find("InputFieldMapX").GetComponent<InputField>();
    InputField inputFieldZ = GameObject.Find("InputFieldMapZ").GetComponent<InputField>();

    inputFieldX.text = levelController.gridX + "";
    inputFieldZ.text = levelController.gridZ + "";
  }

  private string GetSelectedLevel()
  {
    Dropdown dropDownLevels = GameObject.Find("DropdownMapSelection").GetComponent<Dropdown>();
    return dropDownLevels.options[dropDownLevels.value].text;
  }

  private void LoadUIDropdownSetByFileName(string fileName)
  {
    Dropdown dropDownLevels = GameObject.Find("DropdownMapSelection").GetComponent<Dropdown>();
    dropDownLevels.value = dropDownLevels.options.FindIndex(x => x.text == fileName);

  }

  private void LoadUIDropdownFirstLevel()
  {
    Dropdown dropDownLevels = GameObject.Find("DropdownMapSelection").GetComponent<Dropdown>();
    dropDownLevels.value = 1;
  }

  private void LoadUIDropdownOptions()
  {
    Dropdown dropDownLevels = GameObject.Find("DropdownMapSelection").GetComponent<Dropdown>();
    dropDownLevels.ClearOptions();
    dropDownLevels.options.Add(new Dropdown.OptionData("-"));
    List<string> fileNames = fetchLevels.LoadLevelNames(handleLevelFile.directory);
    foreach(string fileName in fileNames)
    {
      Dropdown.OptionData option = new Dropdown.OptionData();
      option.text = fileName;
      dropDownLevels.options.Add(option);
    }
  }
  #endregion
}