using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditor;
using UnityEngine.AI;

namespace Iso
{
    /// <summary>
    /// Each humanoid requires pathfinding.
    /// </summary>
    public class Humanoid : Level_Object, IBaseEntity
    {
        LocationType m_location;

        public NavMeshAgent agent;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}