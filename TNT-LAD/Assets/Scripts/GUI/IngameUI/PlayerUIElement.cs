using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIElement
{
  public GameObject PlayerUI { get; private set; }

  public int PlayerIndex { get; private set; }

  public PlayerUIElement(int playerIndex, GameObject playerUI)
  {
    PlayerIndex = playerIndex;
    PlayerUI = playerUI;
  }
}
