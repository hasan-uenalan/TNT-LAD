using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public GameObject defaultPlayer;
 
  private LevelController levelController;

  private bool isInCoroutine = false;

  void Start()
  {
    //If SPawnPoint broken, FindObjectOfType could be the Problem
    levelController = FindObjectOfType<LevelController>();
    JoinAllPlayers();
  }

  void Update()
  {
    if (CheckGameEnd(out PlayerData winningPlayer) && !isInCoroutine) {
      StaticPlayers.roundOne = false;
      winningPlayer.PlayerScore++;
      StartCoroutine(EndRoundOnWinningPlayer(winningPlayer));      
    }
  }

  private void JoinAllPlayers()
  {
    for(int playerIndex = 0; playerIndex < CrossSceneInformation.PlayerList.Count; playerIndex++)
    {
      Vector3 spawn = new Vector3(levelController.playerSpawns[playerIndex].x, 0.2f, levelController.playerSpawns[playerIndex].y);
      InputDevice inputDevice = CrossSceneInformation.PlayerList[playerIndex].PlayerInputDevice;
      Color playerColor = CrossSceneInformation.PlayerList[playerIndex].PlayerColor;
      JoinPlayer(defaultPlayer, inputDevice, spawn, playerIndex, playerColor);
    }
  }

  private void AddToStaticPlayers(PlayerData playerValues)
  {
    if (StaticPlayers.roundOne) {
      StaticPlayers.Players.Add(playerValues);
    }
  }

  private void JoinPlayer(GameObject player, InputDevice device, Vector3 spawnPoint, int playerIndex, Color playerColor)
  {
    string controlScheme = (device is Gamepad) ? "Gamepad" : "Keyboard";
    PlayerData playerValues = GetPlayerValues(playerIndex + 1, playerColor, spawnPoint);
    AddToStaticPlayers(playerValues);
    PlayerInput playerInput = PlayerInput.Instantiate(player, controlScheme: controlScheme, pairWithDevice: device);
    playerInput.SwitchCurrentControlScheme(controlScheme, new InputDevice[] { device }); //control scheme has to be set twice?
    playerValues.PlayerGameObject = playerInput.gameObject;
    playerValues.PlayerGameObject.GetComponent<PlayerHandler>().PlayerData = playerValues;
    playerValues.PlayerGameObject.GetComponent<PlayerHandler>().SetPlayerToSpawn();
  }


  private PlayerData GetPlayerValues(int playerIndex, Color playerColor, Vector3 spawnPoint)
  {
    var curPlayer = StaticPlayers.Players.FirstOrDefault(x => x.PlayerIndex == playerIndex);
    if(curPlayer != null)
    {
      curPlayer.Lifes = CrossSceneInformation.PlayerLifes;
      return curPlayer;
    }
    return new PlayerData(playerIndex, 0, 1, CrossSceneInformation.PlayerLifes, playerColor, spawnPoint);
  }

  private bool CheckGameEnd(out PlayerData winningPlayer)
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
      winningPlayer = playerListCopy[0];
      AnalyticsHandler.WinnersPowerUps(winningPlayer);
      return true;
    }
    //will never be used but has to be assigned
    winningPlayer = null;
    return false;
  }

  IEnumerator EndRoundOnWinningPlayer(PlayerData winnigPlayer)
  {
    isInCoroutine = true;
    GameObject playerGameObject = winnigPlayer.PlayerGameObject;
    ZoomToPlayer();
    ShowDanceAnimation(playerGameObject);   
    yield return new WaitForSeconds(10); //TODO: wait for dance animation
    SceneManager.LoadScene("RoundScoreboard");
  }

  private void ZoomToPlayer()
  {
    GameObject cameraPivot = GameObject.FindGameObjectWithTag("CameraPivot");
    GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    LeanTween.move(mainCamera, cameraPivot.transform.position, 0.8f).setEaseInOutSine();
    LeanTween.rotate(mainCamera, cameraPivot.transform.rotation.eulerAngles, 0.8f).setEaseInOutSine();
  }

  /// <summary>
  /// insert functionality here
  /// </summary>
  /// <param name="player"></param>
  private void ShowDanceAnimation(GameObject player)
  {
    //if player is not used you can delete the GameObject out of PlayerData
    //you have to disable the movement of the player
    //waiting time in coroutine
  }
}
