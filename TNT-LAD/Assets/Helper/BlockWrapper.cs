using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Class used to add additional information for blocks
[Serializable]
public class BlockWrapper : MonoBehaviour
{
  private Types.blockType blockType;
  public void init(Types.blockType blockType)
  {
    this.blockType = blockType;
  }

  public Types.blockType GetBlockType()
  {
    return blockType;
  }

}
