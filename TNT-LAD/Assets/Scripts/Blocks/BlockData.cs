using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Class used to add additional information for blocks
[Serializable]
public class BlockData : MonoBehaviour
{
  public enum BlockType 
  { 
    DEFAULT, 
    DESTRUCTIBLE,
    WALL
  };

  public BlockType blockType;

  public void Init(BlockType blockType)
  {
    this.blockType = blockType;
  }

  public BlockType GetBlockType()
  {
    return blockType;
  }
}
