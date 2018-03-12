using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Creates a grid for the game using a 2D array.
/// </summary>
public class GridBase : MonoBehaviour
{

    public GameObject nodePrefab;
    public int sizeX;
private NavMeshSurface navSurface;
    public int sizeZ;
    public int offset = 2; //we want to be able to see the nodes/quads invidiually 
    public Transform[] startNavPoints;
    public Node[,] grid;

    private static GridBase instance = null; //there shall only be 1 instance of the gridbase

    /// <summary>
    /// Singleton pattern. Have a static public function for returning the singleton instance.
    /// </summary>
    /// <returns></returns>
    public static GridBase GetInstance() //singleton
    {
        return instance;
    }


    /// <summary>
    /// Create the singleton instance, grid, and collision box.
    /// </summary>
    public void Awake()
    {
        instance = this;
        CreateGrid();
        CreateMouseCollision();
        startNavPoints = new Transform[5];

navSurface = GetComponent<NavMeshSurface>();
        navSurface.BuildNavMesh();
        startNavPoints[0] = grid[0, 0].vis.transform;
        startNavPoints[1] = grid[0, sizeZ-1].vis.transform;
        startNavPoints[2] = grid[sizeX-1, 0].vis.transform;
        startNavPoints[3] = grid[sizeX-1, sizeZ-1].vis.transform;
        startNavPoints[4] = grid[sizeX / 2, sizeZ / 2].vis.transform;
        Debug.Log("Grid Base navSurface finished baking");
    }

    public void Start()
    {
        
    }

    /// <summary>
    /// Create a grid using a nested loop. Each node in the grid is a generic game obect with world coordinates, 
    /// mesh renderer of a quad, and if it's walkable.
    /// </summary>
    void CreateGrid()
    {
        grid = new Node[sizeX, sizeZ];

        for(int x = 0; x < sizeX; x++) //creating nodes on the x axis (happens before pressing play)
        {
            for(int z = 0; z < sizeZ; z++) //creating nodes on the z axis
            {
                float posX = x * offset;
                float posZ = z * offset;

                GameObject go = Instantiate(nodePrefab, new Vector3(posX, 0, posZ), Quaternion.identity) as GameObject;
                go.transform.parent = transform.GetChild(1).transform; //puts this new grid under the floor parent

                NodeObject nodeObj = go.GetComponent<NodeObject>(); //creating each node and setting up their references
                nodeObj.posX = x;
                nodeObj.posZ = z;

                Node node = new Node();
                node.vis = go;
                node.tileRenderer = node.vis.GetComponentInChildren<MeshRenderer>();
                node.isWalkable = true;
                node.nodePosX = x;
                node.nodePosZ = z;
                grid[x, z] = node;
            }
        }
    }

    
    /// <summary>
    /// In order to avoid having x*z amount of colliders, we create a huge box collider the side of x*z
    /// </summary>
    void CreateMouseCollision()
    {
        GameObject go = new GameObject();
        go.AddComponent<BoxCollider>();
        go.GetComponent<BoxCollider>().size = new Vector3(sizeX * offset, 0.1f, sizeZ * offset);
        go.transform.position = new Vector3((sizeX * offset) / 2 - 1, 0, (sizeZ * offset) / 2 - 1); //the ones are magic numbers and should be variables. they're used to make the position in the middle of the quad
    }

    
    /// <summary>
    /// Function returns the appropriate node from the given worldPosition. Essentially like raycasting.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float worldX = worldPosition.x;
        float worldZ = worldPosition.z;

        worldX /= offset;
        worldZ /= offset;

        int x = Mathf.RoundToInt(worldX);
        int z = Mathf.RoundToInt(worldZ);

        if( x > sizeX)
        {
            x = sizeX;
        }
        if(z > sizeZ)
        {
            z = sizeZ;
        }

        if(x < 0)
        {
            x = 0;
        }
        if(z < 0)
        {
            z = 0;
        }

        return grid[x, z];

    }

}
