using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyPlayerValues
{
  public int PlayerNumber { get; set; }

  public bool IsSelectedByPlayer { get; set; }

  public InputDevice PlayerInputDevice { get; set; }

  public GameObject JoinPlayerGameObject { get; set; }

  public LobbyPlayerValues()
  {
    IsSelectedByPlayer = false;
  }

  public void RestoreDefault()
  {
    PlayerNumber = 0;
    IsSelectedByPlayer = false;
    PlayerInputDevice = null;
    JoinPlayerGameObject = null;
  }
}
