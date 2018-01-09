using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//From Sharp Accent tutorial

/// <summary>
/// Class that contains the Node class. Node is the building block for the grid.
/// Grid is made up of Nodes that tell you its position, the tile, if AI can talk on it, the objects currently on it.
/// </summary>
public class Node {

    public int nodePosX;
    public int nodePosZ;
    public GameObject vis; //the quad that's visible
    public MeshRenderer tileRenderer;//for the floor texture
    public bool isWalkable; //for AI
    public LevelEditor.Level_Object placedObj; //the main object on the node
    public List<LevelEditor.Level_Object> stackedObjs = new List<LevelEditor.Level_Object>();
    public LevelEditor.Level_WallObj wallObj;
}
