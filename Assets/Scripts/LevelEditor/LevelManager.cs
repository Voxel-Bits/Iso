using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditor;

/// <summary>
/// This class keeps track of all the game objects in the level using lists (one for walls, stack objects, and non-stack objects.
/// </summary>
public class LevelManager : MonoBehaviour {

    GridBase gridBase;

    public List<GameObject> inSceneGameObjects = new List<GameObject>();
    public List<GameObject> inSceneWalls = new List<GameObject>();
    public List<GameObject> inSceneStackObjects = new List<GameObject>();

    [HideInInspector]
    public GameObject wallHolder;
    [HideInInspector]
    public GameObject objHolder;

    private static LevelManager instance = null;

    /// <summary>
    /// Get the singleton instance of the level manager.
    /// </summary>
    /// <returns></returns>
    public static LevelManager GetInstance()
    {
        return instance;
    }

    /// <summary>
    /// Set the singleton instance of the level manager.
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Gets the singleton instance of the grid base.
    /// </summary>
    void Start()
    {
        wallHolder = new GameObject();
        wallHolder.name = "Wall Holder";
        objHolder = new GameObject();
        objHolder.name = "Obj Holder";

        gridBase = GridBase.GetInstance();

    }

    /// <summary>
    /// 
    /// </summary>
    void InitLevelObjects()
    {

    }

    /// <summary>
    /// Destroy the objects in the level to make way for the level you're going to load.
    /// </summary>
    public void ClearLevel()
    {
        foreach(GameObject g in inSceneGameObjects)
        {
            Destroy(g);
        }

        foreach(GameObject g in inSceneStackObjects)
        {
            Destroy(g);
        }

        foreach(GameObject g in inSceneWalls)
        {
            Destroy(g);
        }

        inSceneWalls.Clear();
        inSceneStackObjects.Clear();
        inSceneGameObjects.Clear();
    }
}
