using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

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

  private GameObject[,] floorMap;
  private GameObject[,] blockMap;
  private GameObject[,] wallMap;

  
  //For scene  structure
  private GameObject floorBlocks;
  private GameObject levelBlocks;
  private GameObject wallBlocks;

  private HandleLevelFile handleLevelFile;

  //public enum blockType {DESTRUCTIBLE, DEFAULT};

  // Start is called before the first frame update
  void Start()
  {
    handleLevelFile = gameObject.GetComponent<HandleLevelFile>();
    

    CreateSceneStructure();
    Construct();
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
    if (File.Exists(handleLevelFile.GetFilePath()))
    {
      updateMapParams();
      blockMap = new GameObject[gridX, gridZ];
      floorMap = new GameObject[gridX, gridZ];
      wallMap = new GameObject[gridX + 2, gridZ + 2];
      PlaceLevelBlocks();
    }
    else
    {
      blockMap = new GameObject[gridX, gridZ];
      floorMap = new GameObject[gridX, gridZ];
      wallMap = new GameObject[gridX + 2, gridZ + 2];
    }
    GenerateFloor();
    GenerateWall();
  }

  private void updateMapParams()
  {
    string[] blockLine = handleLevelFile.GetFileData();
    gridX = blockLine.Length;
    gridZ = blockLine[0].ToCharArray().Length; //whitespace is also part of the array
  }

  //Generates outer wall of play area
  private void GenerateWall()
  {
    for (int z = 0; z < wallMap.GetLength(1); z++)
    {
      if(z != 0 && z != (wallMap.GetLength(1)-1))
      {
        Vector3 leftWallPos = new Vector3((startPos.x + -1) * spacing, blockHeight / 2, (startPos.z + z - 1) * spacing);
        Vector3 rightWallPos = new Vector3((startPos.x + wallMap.GetLength(1) - 2) * spacing, blockHeight / 2, (startPos.z + z - 1) * spacing);
        wallMap[0, z] = Instantiate(outerWall, leftWallPos, Quaternion.identity, wallBlocks.transform);
        wallMap[wallMap.GetLength(1)-1, z] = Instantiate(outerWall, rightWallPos, Quaternion.identity, wallBlocks.transform);
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

  //sets the block at specified location
  public void SetBlock(int x, int z, Types.blockType blockType)
  {
    //out of bounds check
    if(x >= gridX || z >= gridZ)
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

        case Types.blockType.DEFAULT:
          tmp = Instantiate(defaultBlock, pos, Quaternion.identity) as GameObject;
          tmp.AddComponent<BlockWrapper>();
          tmp.GetComponent<BlockWrapper>().init(Types.blockType.DEFAULT);
          blockMap[x, z] = tmp;
          break;
        case Types.blockType.DESTRUCTIBLE:
          tmp = Instantiate(destructibleBlock, pos, Quaternion.identity) as GameObject;
          tmp.AddComponent<BlockWrapper>();
          tmp.GetComponent<BlockWrapper>().init(Types.blockType.DESTRUCTIBLE);
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
  private void PlaceLevelBlocks()
  {

    string[] blockLine = handleLevelFile.GetFileData();

    for (int x = 0; x < blockLine.Length; x++)
    {
      char[] blocks = blockLine[x].ToCharArray();
      for (int z = 0; z < blocks.Length; z++)
      {
        if (blocks[z] == handleLevelFile.GetCharDestructible())
        {
          SetBlock(x, z, Types.blockType.DESTRUCTIBLE);
        }
        if (blocks[z] == handleLevelFile.GetCharDefault())
        {
          SetBlock(x, z, Types.blockType.DEFAULT);
        }
      }
    }
  }

  public void deleteBlock(int x, int z)
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

  private void ClearBlocks(bool clearAll)
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

  [CustomEditor(typeof(LevelController))]
  class DecalMeshHelperEditor : Editor
  {
    LevelController levelController;
    HandleLevelFile handleLevelFile;
    int xBlock = 0;
    int zBlock = 0;

    private void OnEnable()
    {
      levelController = (LevelController)target;
      handleLevelFile = levelController.gameObject.GetComponent<HandleLevelFile>();
    }
    
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      //Seperator
      Rect rect = EditorGUILayout.GetControlRect(false, 1);
      rect.height = 1;
      EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));


      if (Application.isPlaying)
      {
        if (GUILayout.Button("Reconstruct"))
        {
          levelController.Construct();
        }

        //GUILayout.BeginHorizontal();
        xBlock = EditorGUILayout.IntField("X", xBlock, GUILayout.ExpandWidth(false));
        zBlock = EditorGUILayout.IntField("Z", zBlock, GUILayout.ExpandWidth(false));
        //GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Default"))
        {

          levelController.SetBlock(xBlock, zBlock, Types.blockType.DEFAULT);

        }
        if (GUILayout.Button("Destructible"))
        {
         
          levelController.SetBlock(xBlock, zBlock, Types.blockType.DESTRUCTIBLE);

        }
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Delete"))
        {
          levelController.deleteBlock(xBlock, zBlock);
        }
        GUILayout.EndHorizontal();
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Clear Blocks"))
        {
          levelController.ClearBlocks(false);
        }
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Clear All"))
        {
          levelController.ClearBlocks(true);
        }
        if (GUILayout.Button("Delete Level File"))
        {
          handleLevelFile.deleteFile();
        }
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Save Map"))
        {
          handleLevelFile.SaveMapFile(levelController.blockMap);
        }
      }

    }
  }

}
