using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase : MonoBehaviour
{

    public GameObject nodePrefab;

    public int sizeX;
    public int sizeZ;
    public int offset = 1; //we want to be able to see the nodes/quads invidiually 

    public Node[,] grid;

    private static GridBase instance = null; //there shall only be 1 instance of the gridbase
    public static GridBase GetInstance() //singleton
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        CreateGrid();
        CreateMouseCollision();

    }

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

    //Our prefabs for each node will not contain colliders because we'll have x*z amount of colliders
    void CreateMouseCollision()
    {
        GameObject go = new GameObject();
        go.AddComponent<BoxCollider>();
        go.GetComponent<BoxCollider>().size = new Vector3(sizeX * offset, 0.1f, sizeZ * offset);
        go.transform.position = new Vector3((sizeX * offset) / 2 - 1, 0, (sizeZ * offset) / 2 - 1); //the ones are magic numbers and should be variables. they're used to make the position in the middle of the quad
    }

    //raycast hit into node position
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
