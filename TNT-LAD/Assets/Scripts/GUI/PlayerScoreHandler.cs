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
    EnablePlayerComponents();

    foreach (StaticPlayerData staticPlayer in StaticPlayers.staticPlayers) {
      CheckIfPlayerWon(staticPlayer);
      AddPlayerImages(staticPlayer);
      SpawnStars(staticPlayer);
    }
  }
  private void EnablePlayerComponents()
  {
    playerCount = GUIValues.PlayerList.Count;
    for (int i = 0; i < playerCount; i++) {
      gameObject.transform.GetChild(i).gameObject.SetActive(true);
    }
  }

  private void CheckIfPlayerWon(StaticPlayerData staticPlayer)
  {
    if (staticPlayer.PlayerScore >= WinCondition) {
      SceneManager.LoadScene("WinScreen");
    }
  }

  private void AddPlayerImages(StaticPlayerData staticPlayer)
  {

  }

  private void SpawnStars(StaticPlayerData staticPlayer)
  {
    Transform player = gameObject.transform.GetChild(staticPlayer.PlayerIndex - 1);
    player.GetComponent<StarSpawner>().SpawnStars(staticPlayer.PlayerScore, player);
  }
}