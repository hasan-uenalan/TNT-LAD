using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSwitcher : MonoBehaviour
{
  public int selectedPowerUp = 0;
  void Start()
  {
    SelectPowerUp();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      selectedPowerUp = 0;
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      selectedPowerUp = 1;
    }
    SelectPowerUp();
  }

  private void SelectPowerUp()
  {

    int i = 0;
    foreach(Transform powerUp in transform)
    {
      if(i == selectedPowerUp)
      {
        powerUp.gameObject.SetActive(true);
      }
      else
      {
        powerUp.gameObject.SetActive(false);
      }
      i++;
    }
  }

}
