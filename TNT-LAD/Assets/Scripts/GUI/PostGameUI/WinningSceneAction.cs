using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningSceneAction : MonoBehaviour
{
  [Header("Camera")]
  public GameObject cameraPivot;
  public GameObject cameraTarget;
  public float cameraMoveSpeed;

  void Start()
  {
    LeanTween.move(Camera.main.gameObject, cameraPivot.transform.position, cameraMoveSpeed).setEaseInOutSine().setDelay(1f);
  }

  void Update()
  {
    Camera.main.transform.LookAt(cameraTarget.transform.position, Vector3.up);
  }
}
