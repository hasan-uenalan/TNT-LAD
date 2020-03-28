using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

//[ExecuteInEditMode]
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
  public GameObject defaultBlock;
  public GameObject destructibleBlock;
  public float blockHeight;

  [Header("Outer Wall")]
  public GameObject outerWall;

  public GameObject[,] blockMap { set; get; }
  private GameObject[,] floorMap;
  private GameObject[,] wallMap;
  public Vector2[] playerSpawns;

  //For scene  structure
  private GameObject floorBlocks;
  private GameObject levelBlocks;
  private GameObject wallBlocks;

  private HandleLevelFiles handleLevelFile;

  public string currentFile { get; set; }

  //public enum blockType {DESTRUCTIBLE, DEFAULT};

  // Start is called before the first frame update
  void Awake()
  {
    handleLevelFile = new HandleLevelFiles();
    CreateSceneStructure();
    if (SceneManager.GetActiveScene().name == "Editor")
    {
      Construct();
    }
  }

  //creating scene structure
  private void CreateSceneStructure()
  {
    GameObject levelObjects = new GameObject("Level Objects");
    floorBlocks = new GameObject("Floor Blocks");
    floorBlocks.transform.parent = levelObjects.transform;
    levelBlocks = new GameObject("Level Blocks");
    levelBlocks.transform.parent = levelObjects.transform;
    wallBlocks = new GameObject("Wall Blocks");
    wallBlocks.transform.parent = levelObjects.transform;
  }

  //constructs the map
  public void Construct()
  {
    ClearBlocks(true);
    blockMap = new GameObject[gridX, gridZ];
    floorMap = new GameObject[gridX, gridZ];
    wallMap = new GameObject[gridX + 2, gridZ + 2];
    GenerateFloor();
    GenerateWall();
  }

  public void ConstructByFile()
  {
    if (File.Exists(handleLevelFile.GetFilePath(currentFile, ".txt")))
    {
      ClearBlocks(true);
      string[] blockLine = handleLevelFile.GetFileData(currentFile);
      UpdateMapParams(blockLine);
      blockMap = new GameObject[gridX, gridZ];
      floorMap = new GameObject[gridX, gridZ];
      wallMap = new GameObject[gridX + 2, gridZ + 2];
      PlaceLevelBlocks(blockLine);
      GenerateFloor();
      GenerateWall();
      InitPlayerSpawnCoordinates();
    }
    else
    {
      Debug.LogError("Level file doesn't exist");
    }
  }

  public void ConstructCloudLevel(CloudLevel cloudLevel)
  {
    ClearBlocks(true);
    string[] blockLine = cloudLevel.levelContent.Split('\n');
    for(int i = 0; i < blockLine.Length; i++)
    {
      blockLine[i] = blockLine[i].Trim();
    }
    UpdateMapParams(blockLine);
    blockMap = new GameObject[gridX, gridZ];
    floorMap = new GameObject[gridX, gridZ];
    wallMap = new GameObject[gridX + 2, gridZ + 2];
    PlaceLevelBlocks(blockLine);
    GenerateFloor();
    GenerateWall();
    InitPlayerSpawnCoordinates();
  }

  private void UpdateMapParams(string[] blockLine)
  {
    gridX = blockLine.Length;
    gridZ = blockLine[0].ToCharArray().Length; //whitespace is also part of the array
  }

  private void InitPlayerSpawnCoordinates()
  {
    playerSpawns = new Vector2[4];
    Vector2 spawn;
    spawn.x = floorMap[0, 0].transform.position.x;
    spawn.y = floorMap[0, 0].transform.position.z;
    playerSpawns[0] = spawn;
    spawn.x = floorMap[floorMap.GetLength(0) - 1, floorMap.GetLength(1) - 1].transform.position.x;
    spawn.y = floorMap[floorMap.GetLength(0) - 1, floorMap.GetLength(1) - 1].transform.position.z;
    playerSpawns[1] = spawn;
    spawn.x = floorMap[floorMap.GetLength(0) - 1, 0].transform.position.x;
    spawn.y = floorMap[floorMap.GetLength(0) - 1, 0].transform.position.z;
    playerSpawns[2] = spawn;
    spawn.x = floorMap[0, floorMap.GetLength(1) - 1].transform.position.x;
    spawn.y = floorMap[0, floorMap.GetLength(1) - 1].transform.position.z;
    playerSpawns[3] = spawn;

    foreach(Vector2 v in playerSpawns)
    {
    }
  }

  //Generates outer wall of play area
  private void GenerateWall()
  {
    for (int z = 0; z < wallMap.GetLength(1); z++)
    {
      if(z != 0 && z != (wallMap.GetLength(1)-1))
      {
        Vector3 leftWallPos = new Vector3((startPos.x + -1) * spacing, blockHeight / 2, (startPos.z + z - 1) * spacing);
        Vector3 rightWallPos = new Vector3((startPos.x + wallMap.GetLength(0) - 2) * spacing, blockHeight / 2, (startPos.z + z - 1) * spacing);
        wallMap[0, z] = Instantiate(outerWall, leftWallPos, Quaternion.identity, wallBlocks.transform);
        wallMap[wallMap.GetLength(0)-1, z] = Instantiate(outerWall, rightWallPos, Quaternion.identity, wallBlocks.transform);
        continue;
      }
      for (int x = 0; x < wallMap.GetLength(0); x++)
      {
        Vector3 frameWallPos = new Vector3((startPos.x + x - 1) * spacing, blockHeight / 2, (startPos.z + z - 1) * spacing);
        wallMap[x, z] = Instantiate(outerWall, frameWallPos, Quaternion.identity, wallBlocks.transform);
      }
    }
  }

  //generates the Floor of the Gamefield
  private void GenerateFloor()
  {
    for (int x = 0; x < gridX; x++)
    {
      for (int z = 0; z < gridZ; z++)
      {
        Vector3 pos = new Vector3(startPos.x + x * spacing, 0, startPos.z + z * spacing);
        floorMap[x, z] = Instantiate(floor, pos, Quaternion.identity) as GameObject;
        floorMap[x, z].transform.parent = floorBlocks.transform;
      }
    }
  }

  public bool OutOfBoundsCheck(int x, int z)
  {
    if(x >= gridX || z >= gridZ || x < 0 || z < 0)
    {
      return true;
    }
    return false;
  }

  //sets the block at specified location
  public void SetBlock(int x, int z, BlockData.BlockType blockType)
  {
    //out of bounds check
    if(OutOfBoundsCheck(x,z))
    {
      Debug.LogError("Out of bounds! x:" + x + " z: " + z);
      return;
    }
    //placing block only of there is space available
    if(blockMap[x, z] == null)
    {
      Vector3 pos = new Vector3(startPos.x + x * spacing, blockHeight / 2, startPos.z + z * spacing);
      GameObject tmp;
      switch (blockType)
      {

        case BlockData.BlockType.DEFAULT:
          tmp = Instantiate(defaultBlock, pos, Quaternion.identity) as GameObject;
          blockMap[x, z] = tmp;
          break;
        case BlockData.BlockType.DESTRUCTIBLE:
          tmp = Instantiate(destructibleBlock, pos, Quaternion.identity) as GameObject;
          blockMap[x, z] = tmp;
          break;
        default:

          break;
      }
      blockMap[x, z].transform.parent = levelBlocks.transform;
    }
    else
    {
      Debug.LogError("No Space available");
    }
  }

  //places the blocks in the according .txt file
  private void PlaceLevelBlocks(string[] blockLine)
  {
    for (int x = 0; x < blockLine.Length; x++)
    {
      char[] blocks = blockLine[x].ToCharArray();
      for (int z = 0; z < blocks.Length; z++)
      {
        if (blocks[z] == handleLevelFile.charDestructible)
        {
          SetBlock(x, z, BlockData.BlockType.DESTRUCTIBLE);
        }
        if (blocks[z] == handleLevelFile.charDefault)
        {
          SetBlock(x, z, BlockData.BlockType.DEFAULT);
        }
      }
    }
  }

  public void DeleteBlock(int x, int z)
  {
    if(blockMap[x, z] != null)
    {
      Destroy(blockMap[x, z]);
    }
    else
    {
      Debug.LogError("No block at those coordinates");
    }
  }

  public void ClearBlocks(bool clearAll)
  {
    if (blockMap != null)
    {
      for (int x = 0; x < blockMap.GetLength(0); x++)
      {
        for (int z = 0; z < blockMap.GetLength(1); z++)
        {
          Destroy(blockMap[x, z]);
        }
      }
    }
    if (clearAll)
    {
      if (floorMap != null)
      {
        for (int x = 0; x < floorMap.GetLength(0); x++)
        {
          for (int z = 0; z < floorMap.GetLength(1); z++)
          {
            Destroy(floorMap[x, z]);
          }
        }
      }
      if(wallMap != null)
      {
        for (int x = 0; x < wallMap.GetLength(0); x++)
        {
          for (int z = 0; z < wallMap.GetLength(1); z++)
          {
            Destroy(wallMap[x, z]);
          }
        }
      }
    }
  }
}
