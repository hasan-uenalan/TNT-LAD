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
      StaticPlayers.roundOne = false;
      SceneManager.LoadScene("RoundScoreboard");
    }
  }

  private void JoinAllPlayers()
  {
    for(int playerIndex = 0; playerIndex < GUIValues.PlayerList.Count; playerIndex++)
    {
      Vector3 spawn = new Vector3(levelController.playerSpawns[playerIndex].x, 0.2f, levelController.playerSpawns[playerIndex].y);
      InputDevice inputDevice = GUIValues.PlayerList[playerIndex].PlayerInputDevice;
      JoinPlayer(defaultPlayer, inputDevice, spawn, playerIndex);
    }
  }

  private void AddToStaticPlayers(PlayerData playerValues)
  {
    if (StaticPlayers.roundOne) {
      StaticPlayers.Players.Add(playerValues);
    }
  }

  private void JoinPlayer(GameObject player, InputDevice device, Vector3 spawnPoint, int playerIndex)
  {
    string controlScheme = (device is Gamepad) ? "Gamepad" : "Keyboard";
    PlayerData playerValues = GetPlayerValues(playerIndex + 1);
    AddToStaticPlayers(playerValues);
    player.GetComponent<PlayerHandler>().PlayerValues = playerValues;
    PlayerInput playerInput = PlayerInput.Instantiate(player, controlScheme: controlScheme, pairWithDevice: device);
    playerInput.SwitchCurrentControlScheme(controlScheme, new InputDevice[] { device }); //control scheme has to be set twice?
    playerInput.gameObject.GetComponent<PlayerHandler>().PlayerValues = playerValues;
    playerInput.gameObject.transform.position = spawnPoint;
  }

  private PlayerData GetPlayerValues(int playerIndex)
  {
    foreach (PlayerData curPlayer in StaticPlayers.Players) {
      if (curPlayer.PlayerIndex == playerIndex) {
        curPlayer.Lifes = 3;
        return curPlayer;
      }
    }
    return new PlayerData(playerIndex, 0, 1, 3);
  }

  private bool CheckGameEnd()
  {
    List<PlayerData> playerListCopy = new List<PlayerData>(StaticPlayers.Players);
    foreach (PlayerData curPlayer in StaticPlayers.Players) {
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
