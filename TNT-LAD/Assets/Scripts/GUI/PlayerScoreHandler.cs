using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScoreHandler : MonoBehaviour
{
  public int WinCondition = 3;

  private int playerCount;

  void Start()
  {
    StaticPlayers.staticPlayers.Add(new StaticPlayerData(1, 2));
    StaticPlayers.staticPlayers.Add(new StaticPlayerData(2, 0));

    playerCount = 2;//GUIValues.PlayerList.Count;
    for (int i = 0; i < playerCount; i++) {
      gameObject.transform.GetChild(i).gameObject.SetActive(true);
    }
    foreach (StaticPlayerData staticPlayer in StaticPlayers.staticPlayers) {
      CheckIfPlayerWon(staticPlayer);
      Transform player = gameObject.transform.GetChild(staticPlayer.PlayerIndex - 1);
      AddPlayerImages(player);
      SpawnStars(staticPlayer, player);
    }
  }

  private void CheckIfPlayerWon(StaticPlayerData staticPlayer)
  {
    if (staticPlayer.PlayerScore >= WinCondition) {
      SceneManager.LoadScene("WinScreen");
    }
  }

  private void AddPlayerImages(Transform player)
  {
    player.gameObject.SetActive(true);
  }

  private void SpawnStars(StaticPlayerData staticPlayer, Transform player)
  {    
    player.GetComponent<StarSpawner>().SpawnStars(staticPlayer.PlayerScore, player);
  }
}