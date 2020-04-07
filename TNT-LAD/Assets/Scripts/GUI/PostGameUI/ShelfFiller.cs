using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfFiller : MonoBehaviour
{
  public GameObject hatMask;
  public List<GameObject> trophyList;

  public void FillShelf(PlayerData playerData)
  {
    EnableTrophies(playerData.PlayerScore);
    ColorHat(playerData.PlayerColor);
  }

  private void ColorHat(Color playerColor)
  {
    hatMask.GetComponent<Image>().color = playerColor;
  }

  private void EnableTrophies(int playerScore)
  {
    if(playerScore > 3)
    {
      throw new NotSupportedException("Only up to three rounds supported");
    }
    for(int i = 0; i < playerScore; i++)
    {
      trophyList[i].SetActive(true);
    }
  }
}
