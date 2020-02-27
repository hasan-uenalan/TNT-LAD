using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelValues : MonoBehaviour
{

  private int levelNumber { get; set; }

  void Start()
  {
    DontDestroyOnLoad(gameObject);
  }

  public void SetLevelNumber(int levelNumber)
  {
    this.levelNumber = levelNumber;
  }

  public int GetLevelNumber()
  {
    return this.levelNumber;
  }
}
