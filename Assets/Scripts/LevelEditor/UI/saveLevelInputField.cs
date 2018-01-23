using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saveLevelInputField : MonoBehaviour {
    public InputField inputField;
	
    public void SaveLevel()
    {
        Level_SaveLoad.GetInstance().SaveLevelButton(inputField.text);
        inputField.text = "";
        Level_SaveLoad.GetInstance().LoadAllFileLevels();
        InterfaceManager.GetInstance().CloseSaveLevelDialog();
    }
}
