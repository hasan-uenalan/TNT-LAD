using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
  public GameObject defaultPlayer;
 
  private LevelController levelController;

  void Start()
  {
    levelController = FindObjectOfType<LevelController>();
    JoinAllPlayers();
  }

  private void JoinAllPlayers()
  {
    for(int i = 0; i < CrossSceneInformation.PlayerList.Count; i++)
    {
      Vector3 spawn = new Vector3(levelController.playerSpawns[i].x, 0.2f, levelController.playerSpawns[i].y);
      InputDevice inputDevice = CrossSceneInformation.PlayerList[i].PlayerInputDevice;
      JoinPlayer(defaultPlayer, inputDevice, spawn);
    }
  }

  private void JoinPlayer(GameObject player, InputDevice device, Vector3 spawnPoint)
  {
    string controlScheme = (device is Gamepad) ? "Gamepad" : "Keyboard";
    PlayerInput playerInput = PlayerInput.Instantiate(player, controlScheme: controlScheme, pairWithDevice: device);
    playerInput.SwitchCurrentControlScheme(controlScheme, new InputDevice[] { device }); //control scheme has to be set twice?
    playerInput.gameObject.transform.position = spawnPoint;
  }
}
