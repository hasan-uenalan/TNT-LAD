using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class HandleLevelFile : MonoBehaviour
{

    private string filePath;
    private LevelController levelController;

    public void PlaceLevelBlocks()
    {
      levelController = gameObject.GetComponent<LevelController>();

      InitPath();
      var sr = new StreamReader(filePath);
      var fileContents = sr.ReadToEnd();
      sr.Close();

      //reading content
      var blockLine = fileContents.Split("\n"[0]);
      foreach (string line in blockLine)
      {
        print(line);
      }
      for (int x = 0; x < blockLine.Length; x++)
      {
        char[] blocks = blockLine[x].ToCharArray();
        for (int z = 0; z < blocks.Length; z++)
        {
          if (blocks[z] == '+')
          {
            levelController.SetBlock(x, z, LevelController.blockType.DESTRUCTIBLE);
          }
          if (blocks[z] == '*')
          {
            levelController.SetBlock(x, z, LevelController.blockType.DEFAULT);
          }
        }
      }
    }

    private void InitPath()
    {
      filePath = Application.dataPath + "/Ressources/LevelFiles/" + SceneManager.GetActiveScene().name + ".txt";
    }
}
