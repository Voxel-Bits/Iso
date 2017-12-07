using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase : MonoBehaviour
{

    public GameObject nodePrefab;

    public int sizeX;
    public int sizeZ;
    public int offset = 1;

    public Node[,] grid;

    private static GridBase instance = null; //there shall only be 1 instance of the gridbase
    public static GridBase GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        CreateGrid();
        CreateMouseCollision();

    }

}
