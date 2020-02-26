using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerValues : MonoBehaviour
{
  private bool IsSelectedByPlayer { get; set; }

  private void Start()
  {
    SetIsSelected(false);
  }

  public bool getIsSelected()
  {
    return IsSelectedByPlayer;
  }

  public void SetIsSelected(bool isSelectedByPlayer)
  {
    IsSelectedByPlayer = isSelectedByPlayer;
  }

}
