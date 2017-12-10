using System.Collections;
using UnityEngine;

//this class ties the node and nodeObjectSaveable objects together

public class NodeObject : MonoBehaviour {

    public int posX;
    public int posZ;
    public int textureid;

    public void UpdateNodeObject(Node curNode, NodeObjectSaveable saveable)
    {
        posX = saveable.posX;
        posZ = saveable.posZ;

        textureid = saveable.textureId;

        ChangeMaterial(curNode); //for painting the nodes another texture
    }

    void ChangeMaterial(Node curNode)
    {
        Material getMaterial = LevelEditor.ResourcesManager.GetInstance().GetMaterial(textureid);
        curNode.tileRenderer.material = getMaterial;
    }

    public NodeObjectSaveable GetSaveable()
    {
        NodeObjectSaveable saveable = new NodeObjectSaveable();
        saveable.posX = this.posX;
        saveable.posZ = this.posZ;
        saveable.textureId = this.textureid;

        return saveable;
    }


}

[System.Serializable]
public class NodeObjectSaveable
{
    public int posX;
    public int posZ;
    public int textureId;
}
