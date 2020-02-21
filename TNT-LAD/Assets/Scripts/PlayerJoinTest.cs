using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinTest : MonoBehaviour
{
  public bool joinPlayerKeyboard;
  public bool joinPlayerGamepad;
  public GameObject player;

  void Start()
  {

  }

  void Update()
  {
    if (joinPlayerKeyboard)
    {
      joinPlayerKeyboard = false;
      JoinPlayer(Keyboard.current, "Keyboard");
    }
    if (joinPlayerGamepad)
    {
      joinPlayerGamepad = false;
      joinPlayerKeyboard = false;
      JoinPlayer(Gamepad.current, "Gamepad");
    }
  }

  private void JoinPlayer(InputDevice device, string controlScheme)
  {
    PlayerInput.Instantiate(player, controlScheme: controlScheme, pairWithDevice: device);
  }
}
