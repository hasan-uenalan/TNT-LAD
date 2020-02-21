using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
  [Header("Parameters")]
  public int blockSize;
  public float spacing;
  public Vector3 startPos;

  [Header("Grid")]
  public int gridX;
  public int gridZ;

  [Header("Floor")]
  public GameObject floor;

  [Header("Block")]
  public GameObject block;
  public float blockHeight;

  [Header("Outer Wall")]
  public GameObject outerWall;

  private GameObject[,] floorMap;
  private GameObject[,] blockMap;


  // Start is called before the first frame update
  void Start()
  {
    GenerateFloor();
    GenerateWall();

    blockMap = new GameObject[gridX, gridZ];
    setBlock(0, 0);
  }

  //generates the Floor of the Gamefield
  private void GenerateFloor()
  {
    floorMap = new GameObject[gridX, gridZ];
    for (int x = 0; x < gridX; x++)
    {
      for (int z = 0; z < gridZ; z++)
      {
        Vector3 pos = new Vector3(startPos.x + x * spacing, 0, startPos.z + z * spacing);
        floorMap[x, z] = Instantiate(floor, pos, Quaternion.identity) as GameObject;
      }
    }
  }
  //Generates outer wall of play area
  private void GenerateWall()
  {
    //for (int x = -1; x < gridX+1; x++)
    //{
    //  Vector3 pos = new Vector3(startPos.x + x * spacing, 0, startPos.z - spacing);
    //  Instantiate(outerWall, pos, Quaternion.identity);
    //}
  }

  private void setBlock(int x, int z)
  {
    Vector3 pos = new Vector3(startPos.x + x * spacing, blockHeight / 2, startPos.z + z * spacing);
    blockMap[x, z] = Instantiate(block, pos, Quaternion.identity) as GameObject;
  }


  // Update is called once per frame
  void Update()
  {

  }
}
