using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditor;
using UnityEngine.AI;

namespace Iso
{
    /// <summary>
    /// Each humanoid requires pathfinding. This class is for customers and employees.
    /// @member m_location : The location in the game this humanoid is currently located.
    /// @member agent : The navmeshnagent component used for walking around the environment.
    /// @member patrolPoints: The points that the humanoid will be walking to and from.
    /// </summary>
    public class Humanoid : BaseEntity
    {
        LocationType m_location;

        public NavMeshAgent agent;

        public Transform[] patrolPoints; //This will probably chaannggee

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