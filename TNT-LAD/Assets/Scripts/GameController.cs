using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public GameObject defaultPlayer;
 
  private LevelController levelController;

  void Start()
  {
    levelController = FindObjectOfType<LevelController>();
    JoinAllPlayers();
  }

  void Update()
  {
    if (CheckGameEnd()) {
      SceneManager.LoadScene("RoundScoreboard");
    }
  }

  private void JoinAllPlayers()
  {
    for(int i = 0; i < GUIValues.PlayerList.Count; i++)
    {
      Vector3 spawn = new Vector3(levelController.playerSpawns[i].x, 0.2f, levelController.playerSpawns[i].y);
      InputDevice inputDevice = GUIValues.PlayerList[i].PlayerInputDevice;
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

  private bool CheckGameEnd()
  {
    List<PlayerData> playerListCopy = new List<PlayerData>(StaticPlayers.staticPlayers);
    foreach (PlayerData curPlayer in playerListCopy) {
      if(curPlayer.Lifes <= 0) 
      {
        playerListCopy.Remove(curPlayer);
      }
    }
    if(playerListCopy.Count <= 1) 
    {
      playerListCopy[0].PlayerScore++;
      return true;
    }
    return false;
  }
}
