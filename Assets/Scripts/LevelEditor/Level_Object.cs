using System.Collections;
using UnityEngine;


namespace LevelEditor
{

    /// <summary>
    /// This is a class for level objects (not walls), contains world position, ID, 
    /// rotation, reference to its model.
    /// </summary>
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

        int m_ID; //will be used for non-resource manager stuff, it's better to have an int than string
        static int m_iNextValidID; //Next valid ID. value is updated every time class is instantiated

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        public void UpdateNode(Node[,] grid)
        {
            Node node = grid[gridPosX, gridPosZ];
            Vector3 worldPosition = node.vis.transform.position;
            worldPosition += worldPositionOffset;
            transform.rotation = Quaternion.Euler(worldRotation);
            transform.position = worldPosition;
        }


        /// <summary>
        /// Changes the rotation of the object.
        /// </summary>
        public void ChangeRotation()
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles += new Vector3(0, rotateDegrees, 0);
            transform.localRotation = Quaternion.Euler(eulerAngles);
        }


        /// <summary>
        /// Convert the monobehavior level object class into a saveable level object.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Serializable SaveableLevelObject class.
        /// </summary>
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
