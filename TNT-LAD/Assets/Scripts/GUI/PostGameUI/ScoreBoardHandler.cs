using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoardHandler : MonoBehaviour
{
  [HideInInspector] public int WinCondition = 3; //Hidden because it should not be edited from inspector. (Values < 3 not supported by shelf GameObject)
  public List<GameObject> playerShelves;

  void Start()
  {
    CheckIfGameIsOver();
    InitializeShelves();
  }

  private void CheckIfGameIsOver()
  {
    foreach(var player in StaticPlayers.Players)
    {
      if (player.PlayerScore >= WinCondition)
      {
        StaticPlayers.Winner = player;
        SceneManager.LoadScene("WinScreen");
      }
    }
  }

  private void InitializeShelves()
  {
    int playerCount = CrossSceneInformation.PlayerList.Count;
    var yOffset = (playerShelves[1].transform.position.y - playerShelves[0].transform.position.y) * (4 - playerCount);
    gameObject.transform.position += new Vector3(0, yOffset, 0);
    for (int i = 0; i < playerCount; i++)
    {
      InitializeShelf(i);
    }
  }

  private void InitializeShelf(int index)
  {
    playerShelves[index].SetActive(true);
    playerShelves[index].GetComponent<ShelfFiller>().FillShelf(StaticPlayers.Players[index]);
  }
}
