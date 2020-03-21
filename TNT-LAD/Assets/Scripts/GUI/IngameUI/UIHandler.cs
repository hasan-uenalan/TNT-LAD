using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

  public GameObject PlayerUI;
  
  private List<PlayerUIElement> playerUIs;

  // Start is called before the first frame update
  void Start()
  {
    playerUIs = new List<PlayerUIElement>();
    foreach (PlayerData curPlayer in StaticPlayers.Players) 
    {
      GameObject curPlayerUI = GameObject.Instantiate(PlayerUI, gameObject.transform);
      curPlayerUI.transform.localPosition = new Vector3(40f + (curPlayer.PlayerIndex * 110), -18f, 0f);
      curPlayerUI.transform.GetChild(1).GetComponent<Text>().text = "P" + curPlayer.PlayerIndex;
      curPlayerUI.transform.GetChild(3).GetComponent<Text>().text = curPlayer.PlayerScore.ToString();
      playerUIs.Add(new PlayerUIElement(curPlayer.PlayerIndex, curPlayerUI));
    }
  }

  private void Update()
  {
    foreach(PlayerUIElement curPlayerElement in playerUIs) 
    {
      foreach (PlayerData curPlayerData in StaticPlayers.Players) 
      {
        if (curPlayerElement.PlayerIndex == curPlayerData.PlayerIndex) {
          curPlayerElement.PlayerUI.transform.GetChild(2).GetComponent<Text>().text = curPlayerData.Lifes.ToString();
        }
      }
    }
  }
}
