using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterJoiner : MonoBehaviour
{
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
    if (IsConnectionButtonPressed(ref curInputDevice))
    {
      LobbyPlayerValues newPlayer = GetNewPlayerValues(curInputDevice);
      if (newPlayer != null) {
        newPlayer.PlayerInputDevice = curInputDevice;
        playerList.Add(newPlayer);
      }
    }
  }

  private LobbyPlayerValues GetNewPlayerValues(InputDevice inputDevice)
  {
    foreach (GameObject curPlayerSelection in GameObject.FindGameObjectsWithTag("GUIPlayer"))
    {
      LobbyPlayerValues valuesOfCurPlayer = curPlayerSelection.GetComponent<LobbyPlayerValues>();
      if (valuesOfCurPlayer.IsSelectedByPlayer)
      {
        if(valuesOfCurPlayer.PlayerInputDevice == inputDevice)
        {
          playerList.Remove(valuesOfCurPlayer);
          UpdatePlayerOrder(valuesOfCurPlayer);
          UpdateGUI();
          playerCount--;
          return null; //not good to return Null (mahu)
        }
      }
      else
      {
        if (IsInputDeviceUnused(valuesOfCurPlayer))
        {
          valuesOfCurPlayer.IsSelectedByPlayer = true;
          valuesOfCurPlayer.PlayerNumber = playerCount;
          SetGUIComponentsForActivePlayer(valuesOfCurPlayer, curPlayerSelection);
          playerCount++;
          return valuesOfCurPlayer;
        }
      }
    }
    return null;
  }

  private void UpdateGUI()
  {
    foreach (LobbyPlayerValues lobbyPlayer in playerList) {
      GameObject gameObject = GameObject.FindGameObjectsWithTag("GUIPlayer")[lobbyPlayer.PlayerNumber];
      SetGUIComponentsForActivePlayer(lobbyPlayer, gameObject);
    }
    RestoreDefaultGUI(GameObject.FindGameObjectsWithTag("GUIPlayer")[playerList.Count]);
  }

  private void RestoreDefaultGUI(GameObject targetGameObject)
  {
    targetGameObject.transform.GetChild(0).GetComponent<Text>().text = "Press Space/Start\nto Join";
    targetGameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;
    targetGameObject.GetComponent<LobbyPlayerValues>().RestoreDefault();
  }

  private bool IsInputDeviceUnused(LobbyPlayerValues valuesOfCurPlayer)
  {
    foreach (LobbyPlayerValues curLobbyPlayer in playerList) {
      if (curLobbyPlayer.PlayerInputDevice == valuesOfCurPlayer.PlayerInputDevice) {
        return false;
      }
    }
    return true;
  }

  private void UpdatePlayerOrder(LobbyPlayerValues valuesOfCurPlayer)
  {
    foreach (LobbyPlayerValues lobbyPlayer in playerList) {
      int lobbyPlayerNumber = lobbyPlayer.PlayerNumber;
      if (lobbyPlayerNumber > valuesOfCurPlayer.PlayerNumber) {
        lobbyPlayer.PlayerNumber = lobbyPlayerNumber - 1;
      }
    }
  }

  private void SetGUIComponentsForActivePlayer(LobbyPlayerValues valuesOfCurPlayer, GameObject curPlayerSelection)
  {
    curPlayerSelection.transform.GetChild(0).GetComponent<Text>().text = "Player " + valuesOfCurPlayer.PlayerNumber;
    curPlayerSelection.transform.GetChild(1).GetComponent<Image>().enabled = false; //TODO: picture of character
    valuesOfCurPlayer.JoinPlayerGameObject = curPlayerSelection;
  }

  private bool IsConnectionButtonPressed(ref InputDevice inputDevice)
  {
    foreach (InputDevice curInputDevice in InputDevice.all) {
      if(curInputDevice is Keyboard) {
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
