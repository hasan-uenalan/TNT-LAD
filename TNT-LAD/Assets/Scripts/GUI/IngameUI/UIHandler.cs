using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
  [Header("UI-Elements")]
  public GameObject TlUiPrefab;
  public GameObject TrUiPrefab;
  public GameObject BlUiPrefab;
  public GameObject BrUiPrefab;
  [Header("Icons")]
  public GameObject RoundsWonIcon;
  public GameObject LifeIcon;
  
  private List<PlayerUIElement> playerUIs;

  public enum UIPosition
  {
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
  }

  // Start is called before the first frame update
  void Start()
  {
    playerUIs = new List<PlayerUIElement>();
    foreach (PlayerData curPlayer in StaticPlayers.Players) 
    {
      CreatePlayerUI(curPlayer);
    }
  }

  private void Update()
  {
    foreach(PlayerUIElement curPlayerElement in playerUIs) 
    {
      PlayerData curPlayer = StaticPlayers.Players.FirstOrDefault(x => x.PlayerIndex == curPlayerElement.PlayerIndex);
      if(curPlayer == null)
      {
        Debug.LogError("Corresponding Player to UI not found.");
        return;
      }
      curPlayerElement.UpdateUiElement(curPlayer);
    }
  }

  private void CreatePlayerUI(PlayerData playerData)
  {
    GameObject curPlayerUI = null;
    UIPosition uiPosition = GetUiPosition(playerData);
    switch (uiPosition)
    {
      case UIPosition.TopLeft:
        curPlayerUI = Instantiate(TlUiPrefab, gameObject.transform);
        break;
      case UIPosition.TopRight:
        curPlayerUI = Instantiate(TrUiPrefab, gameObject.transform);
        break;
      case UIPosition.BottomLeft:
        curPlayerUI = Instantiate(BlUiPrefab, gameObject.transform);
        break;
      case UIPosition.BottomRight:
        curPlayerUI = Instantiate(BrUiPrefab, gameObject.transform);
        break;
      default:
        Debug.LogError("Invalid UIPosition! (This should not happen)");
        break;
    }
    var playerUi = new PlayerUIElement(playerData.PlayerIndex, curPlayerUI, uiPosition, RoundsWonIcon, LifeIcon);
    playerUi.Initialize(playerData);
    playerUIs.Add(playerUi);
  }

  private UIPosition GetUiPosition(PlayerData playerData)
  {
    UIPosition uiPosition;
    var x = playerData.SpawnPoint.x;
    var z = playerData.SpawnPoint.z;
    if(x == 0) //top
    {
      if(z == 0) //left
      {
        uiPosition = UIPosition.BottomLeft;
      }
      else //right
      {
        uiPosition = UIPosition.BottomRight;
      }
    }
    else //bottom
    {
      if (z == 0) //left
      {
        uiPosition = UIPosition.TopLeft;
      }
      else //right
      {
        uiPosition = UIPosition.TopRight;
      }
    }
    return uiPosition;
  }
}
