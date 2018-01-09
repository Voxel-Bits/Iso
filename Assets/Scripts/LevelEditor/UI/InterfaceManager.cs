using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class manages the inteface. Mainly to check if the mouse is over any UI elements.
/// </summary>
public class InterfaceManager : MonoBehaviour {

    public bool mouseOverUIElement;

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
