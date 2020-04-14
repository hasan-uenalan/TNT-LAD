using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WinningSceneController : MonoBehaviour
{
  public List<GameObject> playerObjects;
  public Text winningPlayer;

  void Start()
  {
    string winnerName = $"Player {StaticPlayers.Winner.PlayerIndex} wins!";
    winningPlayer.text = winnerName;
    ActivatePlayers();

    ResetStatic();
  }

  private void ActivatePlayers()
  {
    var sortedPlayerList = StaticPlayers.Players.OrderByDescending(x => x.PlayerScore).ToList();
    for(int i = 0; i < sortedPlayerList.Count; i++)
    {
      playerObjects[i].SetActive(true);
      ColorHat(playerObjects[i], sortedPlayerList[i].PlayerColor);
    }
  }

  private void ColorHat(GameObject playerObj, Color color)
  {
    playerObj.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
  }

  private void ResetStatic()
  {
    StaticPlayers.Players = new List<PlayerData>();
    StaticPlayers.Winner = null;
    StaticPlayers.roundOne = true;
  }
}
