using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterJoiner : MonoBehaviour
{
  public Button levelButton;
  public GameObject buttonDisabledCross;
  public GameObject playerSelectionPlayerPrefab;

  public List<GameObject> playerMarkers = new List<GameObject>();
  
  private List<LobbyPlayerValues> playerList;

  private void OnDisable()
  {
    GetPlayerColors();
    CrossSceneInformation.PlayerList = playerList;
  }

  private void Start()
  {
    playerList = CrossSceneInformation.PlayerList is null ? new List<LobbyPlayerValues>() : CrossSceneInformation.PlayerList;
  }

  void Update()
  {
    if (IsConnectionButtonPressed(out InputDevice curInputDevice, out string controlScheme)) 
    {
      LobbyPlayerValues newPlayer = GetPlayerInPlayerList(curInputDevice, controlScheme);
      if (newPlayer == null) 
      {
        JoinPlayer(curInputDevice, controlScheme);
      }
      else
      {
        RemovePlayer(newPlayer);
      }
      PositionPlayers();
      UpdateMarkerText();
    }

    if (playerList.Count >= 2) 
    {
      levelButton.interactable = true;
      buttonDisabledCross.SetActive(false);
    }
    else 
    {
      levelButton.interactable = false;
      buttonDisabledCross.SetActive(true);
    }
  }

  private void PositionPlayers()
  {
    for(int i = 0; i < playerList.Count; i++)
    {
      playerList[i].JoinPlayerGameObject.transform.position = playerMarkers[i].transform.GetChild(1).position;
    }
  }

  private void UpdateMarkerText()
  {
    for(int i = 0; i < playerMarkers.Count; i++)
    {
      if(i > playerList.Count - 1)
      {
        playerMarkers[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Press C / N / Start";
        continue;
      }
      playerMarkers[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = $"Player {i+1}";
    }
  }

  private LobbyPlayerValues GetPlayerInPlayerList(InputDevice inputDevice, string controlScheme)
  {
    return playerList.FirstOrDefault(x => (x.PlayerInputDevice == inputDevice && x.ControlScheme == controlScheme));
  }

  private void JoinPlayer(InputDevice inputDevice, string controlScheme)
  {
    LobbyPlayerValues newPlayer = new LobbyPlayerValues { PlayerInputDevice = inputDevice, ControlScheme = controlScheme };
    PlayerInput playerInput = PlayerInput.Instantiate(playerSelectionPlayerPrefab, controlScheme: controlScheme, pairWithDevice: inputDevice);
    playerInput.SwitchCurrentControlScheme(controlScheme, new InputDevice[] { inputDevice }); //control scheme has to be set twice?
    newPlayer.JoinPlayerGameObject = playerInput.gameObject;
    playerList.Add(newPlayer);
  }

  private void RemovePlayer(LobbyPlayerValues lobbyPlayerValues)
  {
    Destroy(lobbyPlayerValues.JoinPlayerGameObject);
    playerList.Remove(lobbyPlayerValues);
  }

  private bool IsConnectionButtonPressed(out InputDevice inputDevice, out string controlScheme)
  {
    inputDevice = null;
    controlScheme = "";
    foreach (InputDevice curInputDevice in InputDevice.all) 
    {
      if (curInputDevice is Keyboard) 
      {
        if (((Keyboard)curInputDevice).cKey.wasPressedThisFrame) 
        {
          inputDevice = curInputDevice;
          controlScheme = "KeyboardWASD";
          return true;
        }
        if (((Keyboard)curInputDevice).nKey.wasPressedThisFrame)
        {
          inputDevice = curInputDevice;
          controlScheme = "KeyboardIJKL";
          return true;
        }
      }
      if (curInputDevice is Gamepad) 
      {
        if (((Gamepad)curInputDevice).startButton.wasPressedThisFrame) 
        {
          inputDevice = curInputDevice;
          controlScheme = "Gamepad";
          return true;
        }
      }
    }
    return false;
  }

  private void GetPlayerColors()
  {
    foreach(var player in playerList)
    {
      player.PlayerColor = player.JoinPlayerGameObject.GetComponent<PlayerCustomization>().hatMat.color;
    }
  }
}
