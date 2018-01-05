using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class InterfaceManager : MonoBehaviour {

    public bool mouseOverUIElement;

    private static InterfaceManager instance = null;


    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        instance = this;
    }


    /// <summary>
    /// 
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
