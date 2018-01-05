using System.Collections;
using UnityEngine;

//this class ties the node and nodeObjectSaveable objects together

/// <summary>
/// 
/// </summary>
public class NodeObject : MonoBehaviour {

    public int posX;
    public int posZ;
    public int textureid;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="curNode"></param>
    /// <param name="saveable"></param>
    public void UpdateNodeObject(Node curNode, NodeObjectSaveable saveable)
    {
        posX = saveable.posX;
        posZ = saveable.posZ;

        textureid = saveable.textureId;

        ChangeMaterial(curNode); //for painting the nodes another texture
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="curNode"></param>
    void ChangeMaterial(Node curNode)
    {
        Material getMaterial = LevelEditor.ResourcesManager.GetInstance().GetMaterial(textureid);
        curNode.tileRenderer.material = getMaterial;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public NodeObjectSaveable GetSaveable()
    {
        NodeObjectSaveable saveable = new NodeObjectSaveable();
        saveable.posX = this.posX;
        saveable.posZ = this.posZ;
        saveable.textureId = this.textureid;

        return saveable;
    }


}

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class NodeObjectSaveable
{
    public int posX;
    public int posZ;
    public int textureId;
}
