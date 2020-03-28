using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
  public float speed = 50;

  // Update is called once per frame
  private void Update()
  {
    transform.Rotate(Vector3.up * speed * Time.deltaTime);
  }
}
