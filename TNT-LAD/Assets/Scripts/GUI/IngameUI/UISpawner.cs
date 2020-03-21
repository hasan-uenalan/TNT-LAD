using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpawner : MonoBehaviour
{

  public GameObject PlayerUI;

  // Start is called before the first frame update
  void Start()
  {
    foreach (PlayerData curPlayer in StaticPlayers.Players) 
    {
      GameObject curPlayerUI = GameObject.Instantiate(PlayerUI, gameObject.transform);
      curPlayerUI.transform.localPosition = new Vector3(30f + (curPlayer.PlayerIndex * 80), -18f, 0f);
      curPlayerUI.transform.GetChild(1).GetComponent<Text>().text = "P" + curPlayer.PlayerIndex;
      curPlayerUI.transform.GetChild(2).GetComponent<Text>().text = curPlayer.Lifes.ToString();
    }
  }
}
