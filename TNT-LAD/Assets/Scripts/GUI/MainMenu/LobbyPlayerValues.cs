using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyPlayerValues
{
  public Color PlayerColor { get; set; }
  public InputDevice PlayerInputDevice { get; set; }
  public string ControlScheme { get; set; }
  public GameObject JoinPlayerGameObject { get; set; }

  public LobbyPlayerValues()
  {

  }

  public void RestoreDefault()
  {
    PlayerInputDevice = null;
    ControlScheme = null;
    JoinPlayerGameObject = null;
  }
}
