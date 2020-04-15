using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioning : MonoBehaviour
{
  void Start()
  {
    CenterCameraPosition();
  }

  public void CenterCameraPosition()
  {
    LevelController lvlController = FindObjectOfType<LevelController>();
    float maxExtends = Mathf.Max(lvlController.gridX, lvlController.gridZ) / 2;
    var cameraPosition = new Vector3(maxExtends, 0, maxExtends);
    maxExtends += 2.5f;
    float angle = (Camera.main.fieldOfView / 2) * Mathf.Deg2Rad;
    float hyp = maxExtends / Mathf.Sin(angle);
    cameraPosition.y = Mathf.Sqrt((Mathf.Pow(hyp, 2) - Mathf.Pow(maxExtends, 2)));
    gameObject.transform.position = cameraPosition;
  }
}
