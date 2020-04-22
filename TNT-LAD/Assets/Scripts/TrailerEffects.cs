using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerEffects : MonoBehaviour
{
  public GameObject wall;
  public GameObject curveShit;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      gameObject.SetActive(false);
      wall.transform.position += new Vector3(0, 0.94f, 0);
    }
    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
      LeanTween.move(wall, wall.transform.position + new Vector3(0, -0.94f, 0), 2.5f);
    }
    if (Input.GetKeyDown(KeyCode.O))
    {
      curveShit.GetComponent<CPC_CameraPath>().enabled = true;
    }
  }
}
