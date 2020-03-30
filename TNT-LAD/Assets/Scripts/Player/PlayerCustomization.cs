using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCustomization : MonoBehaviour
{
  public List<Color> playerColors;
  [HideInInspector] public Material hatMat;
  private int colorIndex;

  void Start()
  {
    hatMat = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;
  }

  private void ColorDown()
  {
    colorIndex--;
    if (colorIndex < 0)
    {
      colorIndex = playerColors.Count - 1;
    }
    hatMat.color = playerColors[colorIndex];
  }

  private void ColorUp()
  {
    colorIndex++;
    if(colorIndex > playerColors.Count - 1)
    {
      colorIndex = 0;
    }
    hatMat.color = playerColors[colorIndex];

  }

  void OnColorSelectionUp(InputValue inputValue)
  {
    var input = inputValue.Get<float>();
    if(input != 0f)
    {
      ColorUp();
    }
  }

  void OnColorSelectionDown(InputValue inputValue)
  {
    var input = inputValue.Get<float>();
    if (input != 0f)
    {
      ColorDown();
    }
  }
}
