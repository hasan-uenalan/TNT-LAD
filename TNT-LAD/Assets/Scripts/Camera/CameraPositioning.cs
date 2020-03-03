using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioning : MonoBehaviour
{
  public Transform cam;
  public float distance;

  private LevelController levelController;

  // Start is called before the first frame update
  void Start()
  {
    levelController = gameObject.GetComponent<LevelController>();
    cam.position = new Vector3(levelController.gridX / 2, distance, levelController.gridZ / 2);
  }
}
