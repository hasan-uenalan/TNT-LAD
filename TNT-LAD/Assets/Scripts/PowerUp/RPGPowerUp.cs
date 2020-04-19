using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPowerUp : MonoBehaviour
{
  public GameObject rocket;
  public Transform firingPoint;


  //// Update is called once per frame
  //void Update()
  //{
  //  if (Input.GetKeyDown(KeyCode.L))
  //  {
  //    Use();
  //  }
  //}

  public void Use()
  {
    Instantiate(rocket, firingPoint.position, firingPoint.rotation);
    //gameObject.SetActive(false);
  }
}
