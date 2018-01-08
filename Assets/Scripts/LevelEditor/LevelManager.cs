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
        gridBase = GridBase.GetInstance();

    }
}
