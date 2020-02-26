using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterJoiner : MonoBehaviour
{
  public List<GameObject> PlayerList;

  private int playerCount = 1;

  private void Start()
  {
    PlayerList = new List<GameObject>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Joystick1Button10)) 
    {
      PlayerList.Add(ActivateNewPlayer());

    }
  }

  private GameObject ActivateNewPlayer()
  {
    foreach (GameObject curPlayerSelection in GameObject.FindGameObjectsWithTag("GUIPlayer")) 
    {
      LobbyPlayerValues valuesOfCurPlayer = curPlayerSelection.GetComponent<LobbyPlayerValues>();
      if (!valuesOfCurPlayer.getIsSelected())
      {
        valuesOfCurPlayer.SetIsSelected(true);
        curPlayerSelection.transform.GetChild(0).GetComponent<Text>().text = "Player " + playerCount;
        playerCount++;
        curPlayerSelection.transform.GetChild(1).GetComponent<Image>().enabled = false;
        return curPlayerSelection;
      }
    }
    return null;
  }
}
