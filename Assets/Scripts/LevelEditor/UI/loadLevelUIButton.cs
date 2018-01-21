using System.Collections;
using UnityEngine.UI;
using LevelEditor;
using UnityEngine;

public class loadLevelUIButton : MonoBehaviour {

    public Text txt;
    public string levelName;

    public void LoadLevel()
    {
        Level_SaveLoad.GetInstance().LoadLevelButton(levelName);
        InterfaceManager.GetInstance().CloseLoadLevelGrid();
    }
}
