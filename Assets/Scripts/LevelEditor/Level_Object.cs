using System.Collections;
using UnityEngine;


namespace LevelEditor
{
    public class Level_Object : MonoBehaviour
    {

        public string obj_Id; //needed for the resources manager
        public int gridPosX; //where on the grid the lvl object is
        public int gridPosY;
        public int gridPosZ;
        public GameObject modelVisualization; //the model
        public Vector3 worldPositionOffset; //offset local position of obj
        public Vector3 worldRotation; //rotation of the model?

        public bool isStackableObj = false;
        public bool isWallObject = false;

        public float rotateDegrees = 90; //how much do you want the model to rotate by when placing in the world

        public void UpdateNode(Node[,] grid)
        {
            Node node = grid[gridPosX, gridPosZ];
            Vector3 worldPosition = node.vis.transform.position;
            worldPosition += worldPositionOffset;
            transform.rotation = Quaternion.Euler(worldRotation);
            transform.position = worldPosition;
        }

        public void ChangeRotation()
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles += new Vector3(0, rotateDegrees, 0);
            transform.localRotation = Quaternion.Euler(eulerAngles);
        }

        public SaveableLevelObject GetSaveableObject()
        {
            SaveableLevelObject savedObj = new SaveableLevelObject();
            savedObj.obj_Id = obj_Id;
            savedObj.posX = gridPosX;
            savedObj.posZ = gridPosZ;

            worldRotation = transform.localEulerAngles;

            savedObj.rotX = worldRotation.x;
            savedObj.rotY = worldRotation.y;
            savedObj.rotZ = worldRotation.z;
            savedObj.isWallObject = isWallObject;
            savedObj.isStackable = isStackableObj;

            return savedObj;
        }
    }
        [System.Serializable]
        public class SaveableLevelObject
        {
            public string obj_Id;
            public int posX;
            public int posZ;

            public float rotX;
            public float rotY;
            public float rotZ;

            public bool isWallObject = false;
            public bool isStackable = false;
        }
    
}
