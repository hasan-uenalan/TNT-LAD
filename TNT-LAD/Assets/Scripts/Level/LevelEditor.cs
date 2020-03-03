using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{

  //For editor
  public bool placeBlocksActive = true;
  private BlockData.BlockType activeBlockType = BlockData.BlockType.DESTRUCTIBLE;

  //necessary components
  private LevelController levelController;

  private int posX;
  private int posZ;

  void Start()
  {
    levelController = gameObject.GetComponent<LevelController>();
  }

  // Update is called once per frame
  void Update()
  {
    SelectObjectMouse();
  }

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

}