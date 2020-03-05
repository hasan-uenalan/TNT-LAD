﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
public class LevelEditor : MonoBehaviour
{

  //For editor
  public bool placeBlocksActive = true;
  private BlockData.BlockType activeBlockType = BlockData.BlockType.DESTRUCTIBLE;

  private string currentFileName = null;

  //necessary components
  private LevelController levelController;
  private HandleLevelFile handleLevelFile;
  private FetchLevels fetchLevels;
  private SceneLoader sceneLoader;

  //Pos for mouse pointer
  private int posX;
  private int posZ;

  void Start()
  {
    levelController = gameObject.GetComponent<LevelController>();
    handleLevelFile = gameObject.GetComponent<HandleLevelFile>();
    fetchLevels = gameObject.GetComponent<FetchLevels>();
    sceneLoader = gameObject.GetComponent<SceneLoader>();

    LoadUIValues();
  }

  // Update is called once per frame
  void Update()
  {
    SelectObjectMouse();
  }

  #region UIStuff (should be eventually moved to own script)
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
  #endregion

  private void SelectObjectMouse()
  {
    if (Input.GetMouseButtonDown(0) && placeBlocksActive)
    {
      Vector2 pos = GetMousePositions();
      posX = Mathf.RoundToInt(pos.x);
      posZ = Mathf.RoundToInt(pos.y);
      if (!levelController.OutOfBoundsCheck(posX, posZ))
      {
        levelController.SetBlock(posX, posZ, activeBlockType);
      }
    }
    if (Input.GetMouseButtonDown(2) && placeBlocksActive)
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

  //for UI elements
  public void SelectDefaultBlock()
  {
    activeBlockType = BlockData.BlockType.DEFAULT;
  }

  public void SelectDestructibleBlock()
  {
    activeBlockType = BlockData.BlockType.DESTRUCTIBLE;
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

  //Reads name of map, saves file and updates currently loaded map
  public void SaveFile()
  {
    handleLevelFile.SaveMapFile(levelController.blockMap, ReadInputFileName());
    LoadUIValues();
  }

  public void UpdateCurrentFile()
  {
    LoadUIValues();
  }

  public void LoadLevel()
  {
    Dropdown dropDownLevels = GameObject.Find("DropdownMapSelection").GetComponent<Dropdown>();
    string levelName = dropDownLevels.options[dropDownLevels.value].text;
    sceneLoader.LoadLevel(levelName);
  }

  private void LoadUIDropdownOptions()
  {
    Dropdown dropDownLevels = GameObject.Find("DropdownMapSelection").GetComponent<Dropdown>();
    dropDownLevels.ClearOptions();
    List<string> fileNames = fetchLevels.LoadLevelNames();
    foreach(string fileName in fileNames)
    {
      Dropdown.OptionData option = new Dropdown.OptionData();
      option.text = fileName;
      dropDownLevels.options.Add(option);
    }
  }

  public void DeleteFile()
  {

  }
}