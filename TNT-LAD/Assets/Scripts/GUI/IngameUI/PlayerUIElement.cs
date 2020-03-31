using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIElement
{
  public int PlayerIndex { get; private set; }
  public GameObject PlayerUI { get; private set; }
  public UIHandler.UIPosition UIPosition { get; set; }

  private GameObject roundsWonIcon { get; set; }
  private GameObject lifeIcon { get; set; }

  public PlayerUIElement(int playerIndex, GameObject playerUI, UIHandler.UIPosition uiPosition, 
    GameObject roundsWonIcon, GameObject lifeIcon)
  {
    PlayerIndex = playerIndex;
    PlayerUI = playerUI;
    UIPosition = uiPosition;
    this.roundsWonIcon = roundsWonIcon;
    this.lifeIcon = lifeIcon;
  }

  public void Initialize(PlayerData playerData)
  {
    PlayerUI.transform.GetChild(2).GetComponent<Text>().text = "Player " + playerData.PlayerIndex;
    //Set player color in UI
    PlayerUI.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = playerData.PlayerColor;
    //Draw rounds/score icons
    DrawIcons(playerData.PlayerScore, PlayerUI.transform.GetChild(3).gameObject, roundsWonIcon);
  }

  public void UpdateUiElement(PlayerData playerData)
  {
    //Draw life icons
    DrawIcons(playerData.Lifes, PlayerUI.transform.GetChild(4).gameObject, lifeIcon);
  }

  private void DrawIcons(int iconCount, GameObject anchor, GameObject sprite)
  {
    if(anchor.transform.childCount == iconCount)
    {
      return;
    }
    ClearChildren(anchor);
    var width = anchor.transform.GetComponent<RectTransform>().sizeDelta.x / 2;
    for (int i = 0; i < iconCount; i++)
    {
      var pos = 
        (UIPosition == UIHandler.UIPosition.TopLeft || UIPosition == UIHandler.UIPosition.BottomLeft) 
        ? anchor.transform.position + new Vector3(i * width, 0, 0)
        : anchor.transform.position - new Vector3(i * width, 0, 0);
      GameObject.Instantiate(sprite, pos, Quaternion.identity, anchor.transform);
    }
  }

  private void ClearChildren(GameObject obj)
  {
    foreach (Transform child in obj.transform)
    {
      GameObject.Destroy(child.gameObject);
    }
  }
}
