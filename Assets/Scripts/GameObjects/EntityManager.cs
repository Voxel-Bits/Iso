using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{

    /// <summary>
    /// Database for instantiated entities. THIS WILL NEED TO BE SERIALIZED AND SHIT. PROBABLY WILL USE REFERENCES.
    /// </summary>
    public class EntityManager : MonoBehaviour {

        SortedDictionary<int, BaseEntity> EntityMap;


        static EntityManager Instance = null;

        /// <summary>
        /// Check if there is a a saved sorted dictionary, if there is, add the key value pairs from there.
        /// </summary>
        void Awake() {
            Instance = this;
            EntityMap = new SortedDictionary<int, BaseEntity>();
        }

        // Update is called once per frame
        void Update() {

        }

        public static EntityManager GetInstance()
        {
            return Instance;
        }

        public void RegisterEntity(BaseEntity NewEntity)
        {
            Debug.Assert(!EntityMap.ContainsKey(NewEntity.m_ID), "EntityManager::RegisterEntity: that entity is already in the database.");
            EntityMap.Add(NewEntity.m_ID, NewEntity);
        }

        public BaseEntity GetEntityFromID(int id)
        {
            Debug.Assert(EntityMap.ContainsKey(id), "EntityManager::RemoveEntity: that entity does not exist in the database.");
            return EntityMap[id];
        }

        public void RemoveEntity(BaseEntity Entity)
        {
            Debug.Assert(EntityMap.ContainsKey(Entity.m_ID), "EntityManager::RemoveEntity: that entity does not exist in the database.");
            EntityMap.Remove(Entity.m_ID);

        } 
    }
}