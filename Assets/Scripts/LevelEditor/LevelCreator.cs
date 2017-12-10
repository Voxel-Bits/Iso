using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{

    public class LevelCreator : MonoBehaviour
    {

        LevelManager manager;
        GridBase gridBase;
        InterfaceManager ui;

        //Place obj variables
        bool hasObj;
        GameObject objToPlace; //the object that gets moved around with the mouse before placing it on the grid
        GameObject cloneObj; 
        Level_Object objProperties;
        Vector3 mousePosition;
        Vector3 worldPosition;
        bool deleteObj;

        //paint tile variables
        bool hasMaterial;
        bool paintTile;
        public Material matToPlace;
        Node previousNode;
        Material prevMaterial;
        Quaternion targetRot;

        //Wall creator variables
        bool createWall;
        public GameObject wallPrefab;
        Node startNode_Wall;
        Node endNodeWall;
        public Material[] wallPlacementMat;
        bool deleteWall;

        void Start()
        {
            gridBase = GridBase.GetInstance();
            manager = LevelManager.GetInstance();
            ui = InterfaceManager.GetInstance();

            PaintAll();
        }

        void Update()
        {
            PlaceObject();
            PaintTile();
            DeleteObjs();
            PlaceStackedObj();
            CreateWall();
            DeleteStackedObjs();
            DeleteWallsActual();
        }

        void UpdateMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit, Mathf.Infinity))
            {
                mousePosition = hit.point;
            }
        }

        #region Place Objects

        //IF the mouse button is clicked place the objects
        void PlaceObject()
        {
            if(hasObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition); //we need to find the node the mouse is over

                worldPosition = curNode.vis.transform.position;

                if(cloneObj == null)
                {
                    cloneObj = Instantiate(objToPlace, worldPosition, Quaternion.identity) as GameObject;
                    objProperties = cloneObj.GetComponent<Level_Object>();

                }
                else
                {
                    cloneObj.transform.position = worldPosition;

                    if(Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                    {
                        //if there's already an object placed on the node, remove it and put the new one
                        if(curNode.placedObj != null)
                        {
                            manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                            Destroy(curNode.placedObj.gameObject);
                            curNode.placedObj = null;
                        }

                        GameObject actualObjPlaced = Instantiate(objToPlace, worldPosition, cloneObj.transform.rotation) as GameObject;
                        Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();

                        placedObjProperties.gridPosX = curNode.nodePosX;
                        placedObjProperties.gridPosZ = curNode.nodePosZ;
                        curNode.placedObj = placedObjProperties;
                        manager.inSceneGameObjects.Add(actualObjPlaced);
                    }

                    if(Input.GetMouseButton(1))
                    {
                        objProperties.ChangeRotation();
                    }
                }
            }
            else
            {
                if(cloneObj != null)
                {
                    Destroy(cloneObj);
                }
            }
        }

        public void PassGameObjectToPlace(string objId)
        {
            //if there's something already in the node, delete it and place the new object
            if(cloneObj != null) 
            {
                Destroy(cloneObj);
            }

            CloseAll();
            hasObj = true;
            cloneObj = null;
            objToPlace = ResourcesManager.GetInstance().GetObjBase(objId).objPrefab;
        }

        void DeleteObjs()
        {
            if(deleteObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if(Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                {
                    //check that the node isn't empty
                    if(curNode.placedObj != null)
                    {
                        if(manager.inSceneGameObjects.Contains(curNode.placedObj.gameObject))
                        {
                            manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                            Destroy(curNode.placedObj.gameObject);
                        }

                        curNode.placedObj = null;
                    }
                }
            }
        }

        public void DeleteObj()
        {
            CloseAll();
            deleteObj = true;
        }

        #endregion

        #region Tile Painting

        #endregion

        #region Stacked Objects

        #endregion

        #region Wall Creation

        #endregion


        void CloseAll()
        {
            hasObj = false;
            deleteObj = false;
            paintTile = false;
            placeStackObj = false;
            createWall = false;
            hasMaterial = false;
            deleteStackObj = false;
            deleteWall = false;
        }
    }

}
