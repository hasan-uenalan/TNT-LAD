using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterJoiner : MonoBehaviour
{
  public Button StartGame; 

  private List<LobbyPlayerValues> playerList;

  private int playerCount = 1;

  private void Start()
  {
    playerList = new List<LobbyPlayerValues>();
  }

  // Update is called once per frame
  void Update()
  {
    InputDevice curInputDevice = null;
    if (IsConnectionButtonPressed(ref curInputDevice)) {
      LobbyPlayerValues newPlayer = GetNewPlayerValues(curInputDevice);
      if (newPlayer != null) {
        newPlayer.PlayerInputDevice = curInputDevice;
        playerList.Add(newPlayer);
      }
      CrossSceneInformation.PlayerList = playerList;
    }
    if (playerList.Count >= 2) {
      StartGame.interactable = true;
    }
    else {
      StartGame.interactable = false;
    }
  }

  //TODO: REFACTORING
  private LobbyPlayerValues GetNewPlayerValues(InputDevice inputDevice)
  {
    LobbyPlayerValues valuesOfCurPlayer = new LobbyPlayerValues();
    foreach (LobbyPlayerValues curLobbyPlayer in playerList) 
    {
      if (IsInputDeviceUnused(inputDevice)) {
        valuesOfCurPlayer.IsSelectedByPlayer = true;
        valuesOfCurPlayer.PlayerNumber = playerCount;
        valuesOfCurPlayer.PlayerInputDevice = inputDevice;
        SetGUIComponentsForActivePlayer(valuesOfCurPlayer);
        playerCount++;
        return valuesOfCurPlayer;
      }
      else {
        if (curLobbyPlayer.PlayerInputDevice == inputDevice) {
          int lobbyPlayerNumber = curLobbyPlayer.PlayerNumber;
          playerList.Remove(curLobbyPlayer);
          UpdatePlayerOrder(lobbyPlayerNumber);
          UpdateGUI();
          playerCount--;
          return null; //not good to return Null (mahu)
        }
      }     
    }
    if (playerList.Count <= 0) {
      valuesOfCurPlayer.IsSelectedByPlayer = true;
      valuesOfCurPlayer.PlayerNumber = playerCount;
      valuesOfCurPlayer.PlayerInputDevice = inputDevice;
      SetGUIComponentsForActivePlayer(valuesOfCurPlayer);
      playerCount++;
      return valuesOfCurPlayer;
    }
    return null;
  }

  private void UpdateGUI()
  {
    var guiPlayer = GameObject.FindGameObjectsWithTag("GUIPlayer");
    foreach (LobbyPlayerValues lobbyPlayer in playerList) {
      GameObject gameObject = guiPlayer[lobbyPlayer.PlayerNumber - 1];
      SetGUIComponentsForActivePlayer(lobbyPlayer);
    }
    RestoreDefaultGUI(guiPlayer[playerList.Count]);
  }

  private void RestoreDefaultGUI(GameObject targetGameObject)
  {
    targetGameObject.transform.GetChild(0).GetComponent<Text>().text = "Press Space/Start\nto Join";
    targetGameObject.transform.GetChild(1).gameObject.SetActive(false);
  }

  private bool IsInputDeviceUnused(InputDevice playerInputDevice)
  {
    foreach (LobbyPlayerValues curLobbyPlayer in playerList) {
      if (curLobbyPlayer.PlayerInputDevice == playerInputDevice) {
        return false;
      }
    }
    return true;
  }

  private void UpdatePlayerOrder(int lobbyPlayerInt)
  {
    foreach (LobbyPlayerValues lobbyPlayer in playerList) {
      int lobbyPlayerNumber = lobbyPlayer.PlayerNumber;
      if (lobbyPlayerNumber > lobbyPlayerInt) {
        lobbyPlayer.PlayerNumber -= 1;
      }
    }
  }

  private void SetGUIComponentsForActivePlayer(LobbyPlayerValues valuesOfCurPlayer)
  {
    GameObject curPlayerSelection = GameObject.FindGameObjectsWithTag("GUIPlayer")[valuesOfCurPlayer.PlayerNumber - 1];
    curPlayerSelection.transform.GetChild(0).GetComponent<Text>().text = "Player " + valuesOfCurPlayer.PlayerNumber;
    curPlayerSelection.transform.GetChild(1).gameObject.SetActive(true); //TODO: picture of character
    valuesOfCurPlayer.JoinPlayerGameObject = curPlayerSelection;
  }

  private bool IsConnectionButtonPressed(ref InputDevice inputDevice)
  {
    foreach (InputDevice curInputDevice in InputDevice.all) {
      if (curInputDevice is Keyboard) {
        if (((Keyboard)curInputDevice).spaceKey.wasPressedThisFrame) {
          inputDevice = curInputDevice;
          return true;
        }
      }
      else if (curInputDevice is Gamepad) {
        if (((Gamepad)curInputDevice).startButton.wasPressedThisFrame) {
          inputDevice = curInputDevice;
          return true;
        }
      }
    }
    return false;
  }
}
