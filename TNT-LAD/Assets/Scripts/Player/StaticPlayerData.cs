using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPlayerData
{
  public int PlayerScore;

  public int PlayerIndex;

  public StaticPlayerData(int playerIndex, int playerScore)
  {
    PlayerIndex = playerIndex;
    PlayerScore = playerScore;
  }
}
