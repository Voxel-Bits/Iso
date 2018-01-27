using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using LevelEditor;
using System;
using UnityEngine;


/// <summary>
/// Class to save and load basic objects. We don't want to save anything from MonoBehavior, which is why we created separate classes.
/// </summary>
public class Level_SaveLoad : MonoBehaviour
{
    public List<String> availableLevels = new List<string>();

    private List<SaveableLevelObject> saveLevelObjects_List = new List<SaveableLevelObject>();
    private List<SaveableLevelObject> saveStackableLevelObjects_List = new List<SaveableLevelObject>();
    private List<NodeObjectSaveable> saveNodeObjectsList = new List<NodeObjectSaveable>();
    private List<WallObjectSaveable> saveWallsList = new List<WallObjectSaveable>();

    public static string saveFolderName = "LevelObjects";

    LevelManager lvlManager;

     /// <summary>
     /// Get singleton instance of the level manager.
     /// </summary>
     void Start()
    {
        lvlManager = LevelManager.GetInstance();

    }

    /// <summary>
    /// Function to assign the "Save Level" button. It saves the objects in the level.
    /// </summary>
    public void SaveLevelButton(string levelName)
    {
        SaveLevel(levelName);
        InterfaceManager.GetInstance().ReloadFiles();
    }


    /// <summary>
    /// Function to assign the "Load Level" button. It loads the objects in the level.
    /// </summary>
    public void LoadLevelButton(string levelName)
    {
        LoadLevel(levelName);
    }


    /// <summary>
    /// Function to return the save location. Creates the directory if it already doesn't exist.
    /// </summary>
    /// <param name="LevelName"></param>
    /// <returns></returns>
    static string SaveLocation(string LevelName, bool save = false)
    {
        string saveLocation = Application.streamingAssetsPath + "/Levels/";

        if(!Directory.Exists(saveLocation))
        {
            Directory.CreateDirectory(saveLocation);
        }
        if (save)
        {
            return saveLocation + "lvl_" + LevelName;

        }
        else
        {

            return saveLocation + LevelName;
        }
    }

    /// <summary>
    /// Get all of the level objects and add them to the appropriate lists. 
    /// Due to how performance intensive this is, it will seem like the editor is stuck, so would probably want a loading bar, etc.
    /// Converts the object information to binary.
    /// </summary>
    /// <param name="saveName"></param>
    void SaveLevel(string saveName)
    {
        Level_Object[] levelObjects = FindObjectsOfType<Level_Object>();

        saveLevelObjects_List.Clear();
        saveWallsList.Clear();
        saveStackableLevelObjects_List.Clear();

        foreach(Level_Object lvlObj in levelObjects)
        {
            if(!lvlObj.isWallObject)
            {
                if (!lvlObj.isStackableObj)
                {
                    saveLevelObjects_List.Add(lvlObj.GetSaveableObject());
                }
                else
                {
                    saveStackableLevelObjects_List.Add(lvlObj.GetSaveableObject());
                }
            }
            else
            {
                WallObjectSaveable w = new WallObjectSaveable();
                w.levelObject = lvlObj.GetSaveableObject();
                w.wallObject = lvlObj.GetComponent<Level_WallObj>().GetSaveable();

                saveWallsList.Add(w);
            }
        }

        NodeObject[] nodeObjects = FindObjectsOfType<NodeObject>();
        saveNodeObjectsList.Clear();

        foreach(NodeObject nodeObject in nodeObjects)
        {
            saveNodeObjectsList.Add(nodeObject.GetSaveable());
        }

        LevelSaveable levelSave = new LevelSaveable();
        levelSave.saveLevelObjects_List = saveLevelObjects_List;
        levelSave.saveStackableObjects_List = saveStackableLevelObjects_List;
        levelSave.saveNodeObjectsList = saveNodeObjectsList;
        levelSave.saveWallsList = saveWallsList;

        string saveLocation = SaveLocation(saveName,true); 

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(saveLocation, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, levelSave);
        stream.Close();

        Debug.Log(saveLocation);
    }


    /// <summary>
    /// Load the level objects into the level. Converts information from binary into object
    /// </summary>
    /// <param name="saveName"></param>
    /// <returns></returns>
    bool LoadLevel(string saveName)
    {
        bool retVal = true;

        string saveFile = SaveLocation(saveName);
        Debug.Log(saveFile);

        if(!File.Exists(saveFile))
        {
            Debug.Log("can't find level");
            retVal = false;
        }
        else
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(saveFile, FileMode.Open);

            LevelSaveable save = (LevelSaveable)formatter.Deserialize(stream);

            stream.Close();
            LoadLevelActual(save);
        }

        return retVal;
    }

    /// <summary>
    /// Go through all of the saveable level objects and assign them to the node that has the same position as the game object in space.
    /// </summary>
    /// <param name="levelSaveable"></param>
    void LoadLevelActual(LevelSaveable levelSaveable)
    {
        LevelManager.GetInstance().ClearLevel();

        #region Create Level Objects
        for(int i = 0; i < levelSaveable.saveLevelObjects_List.Count; i++)
        {
            SaveableLevelObject s_obj = levelSaveable.saveLevelObjects_List[i];

            Node nodeToPlace = GridBase.GetInstance().grid[s_obj.posX, s_obj.posZ];

            GameObject go = Instantiate(
                ResourcesManager.GetInstance().GetObjBase(s_obj.obj_Id).objPrefab,
                nodeToPlace.vis.transform.position,
                Quaternion.Euler(
                    s_obj.rotX, 
                    s_obj.rotY, 
                    s_obj.rotZ))
                    as GameObject;

            nodeToPlace.placedObj = go.GetComponent<Level_Object>();
            nodeToPlace.placedObj.gridPosX = nodeToPlace.nodePosX;
            nodeToPlace.placedObj.gridPosZ = nodeToPlace.nodePosZ;
            nodeToPlace.placedObj.worldRotation = nodeToPlace.placedObj.transform.localEulerAngles;

            lvlManager.inSceneGameObjects.Add(go);
            go.transform.parent = lvlManager.objHolder.transform;
        }
        #endregion

        #region Create Stackable Level Objects
        for(int i = 0; i < levelSaveable.saveStackableObjects_List.Count; i++)
        {
            SaveableLevelObject s_obj = levelSaveable.saveStackableObjects_List[i];
            Node nodeToPlace = GridBase.GetInstance().grid[s_obj.posX, s_obj.posZ];

            GameObject go = Instantiate(
                ResourcesManager.GetInstance().GetStackObjBase(s_obj.obj_Id).objPrefab,
                nodeToPlace.vis.transform.position,
                Quaternion.Euler(
                    s_obj.rotX,
                    s_obj.rotY,
                    s_obj.rotZ))
                    as GameObject;

            Level_Object stack_obj = go.GetComponent<Level_Object>();
            stack_obj.gridPosX = nodeToPlace.nodePosX;
            stack_obj.gridPosZ = nodeToPlace.nodePosZ;

            nodeToPlace.stackedObjs.Add(stack_obj);

            lvlManager.inSceneStackObjects.Add(go);
            go.transform.parent = lvlManager.objHolder.transform;
        }

        #endregion

        #region Paint Tiles
        for(int i = 0; i < levelSaveable.saveNodeObjectsList.Count; i++)
        {
            Node node = GridBase.GetInstance().grid[levelSaveable.saveNodeObjectsList[i].posX,
                levelSaveable.saveNodeObjectsList[i].posZ];

            node.vis.GetComponent<NodeObject>().UpdateNodeObject(node, levelSaveable.saveNodeObjectsList[i]);
        }
        #endregion

        #region Create Walls
        for(int i = 0; i < levelSaveable.saveWallsList.Count; i++)
        {
            WallObjectSaveable s_wall = levelSaveable.saveWallsList[i];

            Node nodeToPlace = GridBase.GetInstance().grid[s_wall.levelObject.posX, s_wall.levelObject.posZ];

            GameObject go = Instantiate(ResourcesManager.GetInstance().wallPrefab,
                nodeToPlace.vis.transform.position,
                Quaternion.identity) as GameObject;

            Level_Object lvlObj = go.GetComponent<Level_Object>();
            lvlObj.gridPosX = nodeToPlace.nodePosX;
            lvlObj.gridPosZ = nodeToPlace.nodePosZ;

            Level_WallObj wall_Obj = go.GetComponent<Level_WallObj>();
            wall_Obj.UpdateWall(s_wall.wallObject.direction);
            wall_Obj.UpdateCorners(
                s_wall.wallObject.corner_a,
                s_wall.wallObject.corner_b,
                s_wall.wallObject.corner_c);

            lvlManager.inSceneWalls.Add(go);
            go.transform.parent = lvlManager.wallHolder.transform;

        }
        #endregion
    }

    /// <summary>
    /// Serializable class that has the lists of level objects. (we can't save Level_SaveLoad class because it derives from Monobehavior)
    /// </summary>
    [Serializable]
    public class LevelSaveable
    {
        public List<SaveableLevelObject> saveLevelObjects_List;
        public List<SaveableLevelObject> saveStackableObjects_List;
        public List<NodeObjectSaveable> saveNodeObjectsList;
        public List<WallObjectSaveable> saveWallsList;
    }


    /// <summary>
    /// Serializable Wall class, which is just a saveable level object and properties specific to walls.
    /// </summary>
    [Serializable]
    public class WallObjectSaveable
    {
        public SaveableLevelObject levelObject;
        public WallObjectSaveableProperties wallObject;
    }

    /// <summary>
    /// This function ensures that you're only loading level files, not .meta files.
    /// </summary>
    public void LoadAllFileLevels()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(Application.streamingAssetsPath + "/Levels");
        FileInfo[] fileInfo = dirInfo.GetFiles();

        foreach(FileInfo f in fileInfo)
        {
            string[] readName = f.Name.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

            if(readName.Length == 2)
            {
                if(string.Equals("lvl", readName[0]))
                {
                    string[] noMeta = readName[1].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

                    if(noMeta.Length == 1)
                    {
                        availableLevels.Add(f.Name);
                    }
                }
            }
        }
    }

    public static Level_SaveLoad instance;

    /// <summary>
    /// Get the singleton instance of the Level_SaveLoad object.
    /// </summary>
    /// <returns></returns>
    public static Level_SaveLoad GetInstance()
    {
        return instance;
    }

    /// <summary>
    /// Set the singleton instance and load all file levels.
    /// </summary>
    void Awake()
    {
        instance = this;
        LoadAllFileLevels();
    }
}
