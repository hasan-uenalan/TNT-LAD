using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScoreHandler : MonoBehaviour
{
  public int WinCondition = 3;

  private int playerCount;

  void Start()
  {
    EnablePlayerComponents();
    foreach (PlayerData staticPlayer in StaticPlayers.Players) {
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

  private void CheckIfPlayerWon(PlayerData staticPlayer)
  {
    if (staticPlayer.PlayerScore >= WinCondition) {
      SceneManager.LoadScene("WinScreen");
    }
  }

  private void AddPlayerImages(PlayerData staticPlayer)
  {

  }

  private void SpawnStars(PlayerData staticPlayer)
  {
    Transform player = gameObject.transform.GetChild(staticPlayer.PlayerIndex - 1);
    for (int curScorePoint = 1; curScorePoint <= staticPlayer.PlayerScore; curScorePoint++) {
      GameObject starPrefab = (GameObject)Resources.Load("GUIComponents/Star");
      GameObject starPic = Instantiate(starPrefab, player);
      starPic.transform.GetComponent<Image>().rectTransform.localPosition = new Vector3(-262 + curScorePoint * 80, 0, 0);
    }
  }
}