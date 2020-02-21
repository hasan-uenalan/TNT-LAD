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

    [Header("Prefabs")]
    public GameObject floor;
    public GameObject block;

    private GameObject[][] map;
    

    // Start is called before the first frame update
    void Start()
    {
        for(int x=0; x < gridX; x++)
        {
            for(int y=0; x < gridZ; y++)
            {
                Vector3 pos = new Vector3(startPos.x + x * spacing, 0, startPos.y + y * spacing);
                map[x][y] = Instantiate(floor, pos, Quaternion.identity) as GameObject;
            }
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
