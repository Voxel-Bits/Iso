using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{

    /// <summary>
    /// All humanoids will have the ability to patrol between given points. Singleton.
    /// </summary>
    public class PatrolState : State<Humanoid>
    {
        private int destPoint = 0;

        private static PatrolState Instance = null;

        public static PatrolState GetInstance()
        {

            return Instance;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public override void Enter(Humanoid entity)
        {
            Debug.Assert(entity.agent != null, "PatrolState:: Enter: Entity's navmesh agent must not be null");

        }

        /// <summary>
        /// Customer to move in the world space. Walking animation to loop.
        /// Go through the patrol points and randomly go to one
        /// </summary>
        /// <param name="entity"></param>
        public override void Execute(Humanoid entity)
        {
            if(!entity.agent.pathPending && entity.agent.remainingDistance < 0.5f)
            {
                GoToNextPoint(entity);
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public override void Exit(Humanoid entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="telegram"></param>
        /// <returns></returns>
        public override bool OnMessage(Humanoid entity, Telegram telegram)
        {
            throw new NotImplementedException();
        }

        private void Start()
        {
            
        }

        // Use this for initialization
        void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// If an object is added on the node the point is in, search the nearby nodes to see if one is available to reassign patrol point to
        /// ->If there are multiple points available, choose the one that is the furthest away from the adjacent points, and/or the furthest away from the 'middle' point
        /// --> If there is are still multiple of those, choose a random one
        /// </summary>
        private void GoToNextPoint(Humanoid entity)
        {
            if(entity.pPoints.points.Length == 0)
            {
                return;
            }

            entity.agent.destination = entity.pPoints.points[destPoint].position;
            destPoint = (destPoint + 1) % entity.pPoints.points.Length;
        }

    }
}
