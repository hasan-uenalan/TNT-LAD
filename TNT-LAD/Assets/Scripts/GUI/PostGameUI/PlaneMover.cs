using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMover : MonoBehaviour
{
  public float planeSpeed;
  private GameObject propeller;

  private void Start()
  {
    propeller = gameObject.transform.GetChild(0).gameObject;
  }

  private void FixedUpdate()
  {
    propeller.transform.Rotate(propeller.transform.forward, Time.deltaTime * 2000f);
    gameObject.transform.position += (gameObject.transform.forward * (planeSpeed * Time.deltaTime));
  }
}
