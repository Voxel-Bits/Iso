using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{
    /// <summary>
    /// This class will be used for updating patrol points. 
    /// Points will only be updated if an event happens.
    /// </summary>
    public class PatrolPoints : MonoBehaviour
    {

        Transform[] ptrlPts;
        GridBase gridBase;


        /// <summary>
        /// When floor is empty, start off with 4 points on each respective corner of the floor.
        /// </summary>
        void Start()
        {
            gridBase = GridBase.GetInstance();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// If an object is added on the node the point is in, search the nearby nodes to see if one is available to reassign patrol point to
        /// ->If there are multiple points available, choose the one that is the furthest away from the adjacent points, and/or the furthest away from the 'middle' point
        /// --> If there is are still multiple of those, choose a random one
        /// </summary>
        /// <param name="patrolPoint"></param>
        void UpdatePatrolPoint(Transform patrolPoint)
        {

        }
    }
}
