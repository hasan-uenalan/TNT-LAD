using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyPlayerValues : MonoBehaviour
{
  public int PlayerNumber { get; set; }

  public bool IsSelectedByPlayer { get; set; }

  public InputDevice PlayerInputDevice { get; set; }

  public GameObject JoinPLayerGameObject { get; set; }

  void Start()
  {
    IsSelectedByPlayer = false;
  }
}
