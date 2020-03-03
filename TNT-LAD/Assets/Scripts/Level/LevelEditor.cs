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

  void Start()
  {
    levelController = gameObject.GetComponent<LevelController>();
  }

  // Update is called once per frame
  void Update()
  {
    selectObjectMouse();
  }

  private void selectObjectMouse()
  {
    if (Input.GetMouseButtonDown(0) && placeBlocksActive)
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, 100))
      {
        int posX = Mathf.RoundToInt(hit.transform.gameObject.transform.position.x);
        int posZ = Mathf.RoundToInt(hit.transform.gameObject.transform.position.z);
        levelController.SetBlock(posX, posZ, activeBlockType);
      }
    }
  }



}
