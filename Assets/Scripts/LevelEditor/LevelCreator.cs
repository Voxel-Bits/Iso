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
        Quaternion prevRotation;

        //place stack obj variables
        bool placeStackObj;
        GameObject stackObjToPlace;
        GameObject stackCloneObj;
        Level_Object stackObjProperties;
        bool deleteStackObj;

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
            DeleteStackObjs();
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

        void PaintTile()
        {
            if(hasMaterial)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if(previousNode == null)
                {
                    previousNode = curNode;
                    prevMaterial = previousNode.tileRenderer.material;
                    prevRotation = previousNode.vis.transform.rotation;
                }
                else
                {
                    if(previousNode != curNode)
                    {
                        if(paintTile)
                        {
                            int matId = ResourcesManager.GetInstance().GetMaterialId(matToPlace); //Get the material from our list
                            curNode.vis.GetComponent<NodeObject>().textureid = matId; //assign the curNode's material with the one we want
                            paintTile = false;
                        }
                        else
                        {
                            previousNode.tileRenderer.material = prevMaterial;
                            previousNode.vis.transform.rotation = prevRotation;
                        }

                        previousNode = curNode;
                        prevMaterial = curNode.tileRenderer.material;
                        prevRotation = curNode.vis.transform.rotation;
                    }
                }

                curNode.tileRenderer.material = matToPlace; //the curNode will be rendering the material we want to place, but it hasn't assigned it as the mat yet
                curNode.vis.transform.localRotation = targetRot;

                //only if the left mouse button is clicked and then goes away from that tile will it be assigned the new mat
                if(Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                {
                    paintTile = true;
                }

                if(Input.GetMouseButtonUp(1))
                {
                    Vector3 eulerAngles = curNode.vis.transform.eulerAngles;
                    eulerAngles += new Vector3(0, 90, 0);
                    targetRot = Quaternion.Euler(eulerAngles);
                }
            }
        }

        public void PassMaterialToPaint(int matId)
        {
            deleteObj = false;
            placeStackObj = false;
            hasObj = false;
            matToPlace = ResourcesManager.GetInstance().GetMaterial(matId);
            hasMaterial = true;
        }

        public void PaintAll()
        {
            for(int x = 0; x < gridBase.sizeX; x++)
            {
                for(int z = 0; z < gridBase.sizeZ; z++)
                {
                    gridBase.grid[x, z].tileRenderer.material = matToPlace;
                    int matId = ResourcesManager.GetInstance().GetMaterialId(matToPlace);
                    gridBase.grid[x, z].vis.GetComponent<NodeObject>().textureid = matId;
                }
            }

            previousNode = null;
        }

        #endregion

        #region Stacked Objects

        public void PassStackedObjectToPlace(string objId)
        {
            if(stackCloneObj != null)
            {
                Destroy(stackCloneObj);

            }

            CloseAll();
            placeStackObj = true;
            stackCloneObj = null;
            stackObjToPlace = ResourcesManager.GetInstance().GetStackObjBase(objId).objPrefab;

        }

        void PlaceStackedObj()
        {
            if(placeStackObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition); //find the node the mouse is over

                worldPosition = curNode.vis.transform.position;

                if(stackCloneObj == null)
                {
                    stackCloneObj = Instantiate(stackObjToPlace, worldPosition, Quaternion.identity) as GameObject;
                    stackObjProperties = stackCloneObj.GetComponent<Level_Object>();

                }
                else
                {
                    stackCloneObj.transform.position = worldPosition; //place it on the worldposition

                    if(Input.GetMouseButtonUp(0) && !ui.mouseOverUIElement)
                    {
                        GameObject actualObjPlaced = Instantiate(stackObjToPlace, worldPosition, stackCloneObj.transform.rotation) as GameObject;
                        Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();

                        placedObjProperties.gridPosX = curNode.nodePosX;
                        placedObjProperties.gridPosZ = curNode.nodePosZ;
                        curNode.stackedObjs.Add(placedObjProperties);
                        manager.inSceneGameObjects.Add(actualObjPlaced);
                    }

                    if(Input.GetMouseButtonUp(1))
                    {
                        stackObjProperties.ChangeRotation();
                    }
                }

            }
            else
            {
                if(stackCloneObj != null)
                {
                    Destroy(stackCloneObj);
                }
            }

        }

        public void DeleteStackObj()
        {
            CloseAll();
            deleteStackObj = true;
        }

        void DeleteStackObjs()
        {
            if(deleteStackObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if(Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                {
                    if(curNode.stackedObjs.Count > 0)
                    {
                        for(int i = 0; i < curNode.stackedObjs.Count; i++)
                        {
                            if(manager.inSceneStackObjects.Contains(curNode.stackedObjs[i].gameObject))
                            {
                                manager.inSceneStackObjects.Remove(curNode.stackedObjs[i].gameObject);
                                Destroy(curNode.stackedObjs[i].gameObject);
                            }
                        }

                        curNode.stackedObjs.Clear();
                    }
                }
            }
        }

        #endregion

        #region Wall Creation


        //Function for wall creation button
        public void OpenWallCreation()
        {
            CloseAll();
            createWall = true;
        }

        void CreateWall()
        {
            if(createWall)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                worldPosition = curNode.vis.transform.position;

                //If we haven't clicked at all yet
                if(startNode_Wall == null)
                {
                    //the first time you clicked is the first part of the wall
                    if(Input.GetMouseButtonUp(0) && !ui.mouseOverUIElement)
                    {
                        startNode_Wall = curNode;
                    }
                }else
                {
                    //clicking again 
                    if(Input.GetMouseButtonUp(0) && !ui.mouseOverUIElement)
                    {
                        endNodeWall = curNode;
                    }
                }


                //we only have the start node and the end node, how do we determine what the end result will look like?
                if(startNode_Wall != null && endNodeWall !=null)
                {
                    //get the differences between the x and y positions of both nodes, then we will know how far away they are
                    int difX = endNodeWall.nodePosX - startNode_Wall.nodePosX;
                    int difZ = endNodeWall.nodePosZ - startNode_Wall.nodePosZ;

                    CreateWallInNode(startNode_Wall.nodePosX, startNode_Wall.nodePosZ, Level_WallObj.WallDirection.ab);

                    Node finalXNode = null;
                    Node finalZNode = null;

                    //check only for x differences, if the player didn't just click on the same node.
                    //This only checks if they do a horizontal wall placement
                    if(difX != 0)
                    {
                        bool xHigher = (difX > 0);

                        //plus 1 so it doesn't ignore the last node
                        for(int i = 1; i < Mathf.Abs(difX) + 1; i++)
                        {
                            int offset = xHigher ? i : -i; //this is so we don't need two for loops
                            int posX = startNode_Wall.nodePosX + offset;
                            int posZ = startNode_Wall.nodePosZ;

                            if(posX < 0)
                            {
                                posX = 0;
                            }
                            if(posX > gridBase.sizeX)
                            {
                                posX = gridBase.sizeX;
                            }
                            if(posZ < 0)
                            {
                                posZ = 0;
                            }
                            if(posZ > gridBase.sizeZ)
                            {
                                posZ = gridBase.sizeZ;
                            }

                            finalXNode = gridBase.grid[posX, posZ];

                            Level_WallObj.WallDirection targetDir = Level_WallObj.WallDirection.ab;

                            CreateWallInNode(posX, posZ, targetDir);

                        }

                        UpdateWallCorners(xHigher ? endNodeWall : startNode_Wall, true, false, xHigher ? false : false);

                        UpdateWallCorners(xHigher ? startNode_Wall : endNodeWall, false, true, xHigher ? false : false);
                    }
                }
            }
        }

        void CreateWallOrUpdateNode(Node getNode, Level_WallObj.WallDirection direction)
        {

        }

        Node DestroyCurentNodeAndGetPrevious(Node curNode, bool positive)
        {

        }

        //actually create the wall in the node
        void CreateWallInNode(int posX, int posZ, Level_WallObj.WallDirection direction)
        {
            Node getNode = gridBase.grid[posX, posZ];

            Vector3 wallPosition = getNode.vis.transform.position;

            //if there's no wall in the node yet
            if(getNode.wallObj == null)
            {
                GameObject actualObjPlaced = Instantiate(wallPrefab, wallPosition, Quaternion.identity) as GameObject;
                Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();
                Level_WallObj placedWallProperties = actualObjPlaced.GetComponent<Level_WallObj>();

                placedObjProperties.gridPosX = posX;
                placedObjProperties.gridPosZ = posZ;
                manager.inSceneWalls.Add(actualObjPlaced);
                getNode.wallObj = placedWallProperties;

                UpdateWallNode(getNode, direction);
            }else
            {
                //we don't need to update the wall properties, so we only update the corners as necIessary
                UpdateWallNode(getNode, direction);
            }

            UpdateWallCorners(getNode, false, false, false);
        }

        void UpdateWallNode(Node getNode, Level_WallObj.WallDirection direction)
        {

            getNode.wallObj.UpdateWall(direction);
            
        }

        void UpdateWallCorners(Node getNode, bool a, bool b, bool c)
        {
            if(getNode.wallObj != null)
            {
                getNode.wallObj.UpdateCorners(a, b, c);
            }
        }

        public void DeleteWall()
        {
            CloseAll();
            deleteWall = true;
        }

        void DeleteWallsActual()
        {

        }
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
