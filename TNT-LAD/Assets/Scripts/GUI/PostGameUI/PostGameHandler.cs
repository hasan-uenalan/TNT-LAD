using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostGameHandler : MonoBehaviour
{

  void Start()
  {
    string winnerName = "Player " + StaticPlayers.Winner.PlayerIndex.ToString();
    gameObject.GetComponent<Text>().text = winnerName;

    ResetStatic();
  }

  private void ResetStatic()
  {
    StaticPlayers.Players = new List<PlayerData>();
    StaticPlayers.Winner = null;
    StaticPlayers.roundOne = true;
  }
}
