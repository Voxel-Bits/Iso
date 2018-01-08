using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    
    /// <summary>
    /// Manages the resources of game level objects, the stacking game level objects, level materials, and wall prefabs.
    /// </summary>
    public class ResourcesManager : MonoBehaviour
    {
        public List<LevelGameObjectBase> LevelGameObjects = new List<LevelGameObjectBase>();
        public List<LevelStackedObjsBase> LevelGameObjects_Stacking = new List<LevelStackedObjsBase>(); //stacking means we don't care if we have multiple gameobjs on node
        public List<Material> LevelMaterials = new List<Material>(); //all mats on terrain
        public GameObject wallPrefab;

        private static ResourcesManager instance = null;


        /// <summary>
        /// Initialize the Resources Manager singleton.
        /// </summary>
        void Awake()
        {
            instance = this;
        }


        /// <summary>
        /// Returns the instance of the Resources Manager
        /// </summary>
        /// <returns></returns>
        public static ResourcesManager GetInstance()
        {
            return instance;
        }


        /// <summary>
        /// Returns the LevelGameObjectBase from the given object ID. 
        /// Used to check if the lvl game obj base is already in the level.
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public LevelGameObjectBase GetObjBase(string objId)
        {
            LevelGameObjectBase retVal = null;

            for(int i = 0; i < LevelGameObjects.Count; i++)
            {
                if(objId.Equals(LevelGameObjects[i].obj_id))
                {
                    retVal = LevelGameObjects[i];
                    break;
                }
            }

            return retVal;
        }

        

        /// <summary>
        /// Returns the LevelStackedObjsBase using the given stack ID. 
        /// Used to check if the level stacked obj base is already in the level.
        /// </summary>
        /// <param name="stack_id"></param>
        /// <returns></returns>
        public LevelStackedObjsBase GetStackObjBase(string stack_id)
        {
            LevelStackedObjsBase retVal = null;
            for(int i = 0; i < LevelGameObjects_Stacking.Count; i ++)
            {
                if(stack_id.Equals(LevelGameObjects_Stacking[i].stack_id))
                {
                    retVal = LevelGameObjects_Stacking[i];
                    break;
                }
            }

            return retVal;
        }


        /// <summary>
        /// Retrn the material with the corresponding ID.
        /// </summary>
        /// <param name="matId"></param>
        /// <returns></returns>
        public Material GetMaterial(int matId)
        {
            Material retVal = null;

            for(int i = 0; i < LevelMaterials.Count; i++)
            {
                if(matId == i)
                {
                    retVal = LevelMaterials[i];
                    break;
                }

            }

            return retVal;
        }


        /// <summary>
        /// Return the material ID with the given material.
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int GetMaterialId(Material mat)
        {
            int id = -1;

            for(int i = 0; i < LevelMaterials.Count; i++)
            {
                if(mat.Equals(LevelMaterials[i]))
                {
                    id = i;
                    break;
                }
            }

            return id;
        }

    }


    /// <summary>
    /// Serializable LevelGameObjectBase class.
    /// </summary>
    [System.Serializable]
    public class LevelGameObjectBase
    {
        public string obj_id;
        public GameObject objPrefab;
    }


    /// <summary>
    /// Serializable LevelStackedObjsBase class.
    /// </summary>
    [System.Serializable]
    public class LevelStackedObjsBase
    {
        public string stack_id;
        public GameObject objPrefab;
    }
}
