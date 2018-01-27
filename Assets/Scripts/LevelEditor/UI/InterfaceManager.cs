using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;


/// <summary>
/// This class manages the inteface. Mainly to check if the mouse is over any UI elements.
/// </summary>
public class InterfaceManager : MonoBehaviour {

    public bool mouseOverUIElement;

    public bool levelEditorMode;

    public Transform loadLevelGrid;
    public Transform saveLevelDialog;
    public Transform levelEditorCanvas;
    public Transform enableLevelEditorButton;
    public Transform disableLevelEditorButton;

    public GameObject[] otherUI;

    public GameObject loadLevelUIbuttonPrefab;

    Level_SaveLoad sl;


    /// <summary>
    /// Make the save level dialog and level grid invisible.
    /// </summary>
    void Start()
    {
        saveLevelDialog.gameObject.SetActive(false);
        CloseLoadLevelGrid();

        sl = Level_SaveLoad.GetInstance();

        CreateUIButtonsForAvailableLevels();

        if(!levelEditorMode)
        {
            levelEditorCanvas.gameObject.SetActive(false);
        }else
        {
            levelEditorCanvas.gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// Create a button for each level.
    /// </summary>
    void CreateUIButtonsForAvailableLevels()
    {
        foreach(string s in sl.availableLevels)
        {
            GameObject go = Instantiate(loadLevelUIbuttonPrefab) as GameObject;
            go.transform.SetParent(loadLevelGrid);
            loadLevelUIButton b = go.GetComponent<loadLevelUIButton>();
            b.levelName = s;
            b.txt.text = s;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (levelEditorMode)
        {
            levelEditorCanvas.gameObject.SetActive(true);
            disableLevelEditorButton.gameObject.SetActive(true);
            enableLevelEditorButton.gameObject.SetActive(false);
            mouseOverUIElement = EventSystem.current.IsPointerOverGameObject();
        }else
        {
            levelEditorCanvas.gameObject.SetActive(false);
            enableLevelEditorButton.gameObject.SetActive(true);
            disableLevelEditorButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Function for Load Level button to call and close or open the load level grid accordingly.
    /// </summary>
    public void OpenCloseLevelGrid()
    {
        if(loadLevelGrid.gameObject.activeInHierarchy)
        {
            CloseLoadLevelGrid();
        }
        else
        {
            OpenLoadLevelGrid();
        }
    }

    /// <summary>
    /// Set the load level grid to active.
    /// </summary>
    public void OpenLoadLevelGrid()
    {
        loadLevelGrid.gameObject.SetActive(true);
    }

    /// <summary>
    /// Close the UI grid that lists all of the available levels to load.
    /// </summary>
    public void CloseLoadLevelGrid()
    {
        loadLevelGrid.gameObject.SetActive(false);
    }

    /// <summary>
    /// Opens the save level dialog UI.
    /// </summary>
    public void OpenSaveLevelDialog()
    {
        saveLevelDialog.gameObject.SetActive(true);
        foreach(GameObject go in otherUI)
        {
            go.SetActive(false);
        }

        loadLevelGrid.gameObject.SetActive(false);
    }

    /// <summary>
    /// Closes the save level dialog UI.
    /// </summary>
    public void CloseSaveLevelDialog()
    {
        saveLevelDialog.gameObject.SetActive(false);

        foreach(GameObject go in otherUI)
        {
            go.SetActive(true);
        }

        loadLevelGrid.gameObject.SetActive(true);
    }

    private static InterfaceManager instance = null;


    /// <summary>
    /// Initializes the singleton instance of the Interface manager.
    /// </summary>
    void Awake()
    {
        instance = this;

    }


    /// <summary>
    /// Returns the singleton instance of the Interface manager.
    /// </summary>
    /// <returns></returns>
    public static InterfaceManager GetInstance()
    {
        return instance;
    }

    /// <summary>
    /// Function for the 'New Level' button.
    /// </summary>
    public void NewLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    /// <summary>
    /// Reload the level buttons in the load level grid. 
    /// This is used for when a level is saved since a new button needs to be added.
    /// </summary>
    public void ReloadFiles()
    {
        Button[] prevB = loadLevelGrid.GetComponentsInChildren<Button>();
        foreach(Button b in prevB)
        {
            Destroy(b.gameObject);
        }

        sl.availableLevels.Clear();

        sl.LoadAllFileLevels();
        CreateUIButtonsForAvailableLevels();
    }


    /// <summary>
    /// From non-level editor mode, click button to enter level editor.
    /// </summary>
    public void EnterLevelEditorModeButton()
    {
        levelEditorMode = true;
    }


    /// <summary>
    /// From level editor mode, click button to exit lvl editor.
    /// </summary>
    public void ExitLevelEditorModeButton()
    {
        levelEditorMode = false;
    }

    /// <summary>
    /// 
    /// </summary>
    public void MouseEnter()
    {
        mouseOverUIElement = true;
    }


    /// <summary>
    /// 
    /// </summary>
    public void MouseExit()
    {
        mouseOverUIElement = false;
    }
}
