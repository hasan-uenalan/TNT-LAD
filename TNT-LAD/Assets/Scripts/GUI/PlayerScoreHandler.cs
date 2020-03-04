using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreHandler : MonoBehaviour
{

  private int playerCount;

  void Start()
  {
    playerCount = 4;//GUIValues.PlayerList.Count;
    for (int i = 0; i < playerCount; i++) {
      gameObject.transform.GetChild(i).gameObject.SetActive(true);
    }
  }

  void Update()
  {

  }
}