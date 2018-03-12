using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditor;
using UnityEngine.AI;
using System;

namespace Iso
{
    public struct PatrolPoints
    {
        public Transform[] points;
        public EntityType entityType;

        public PatrolPoints(Transform[] patrolPoints, EntityType eType)
        {
            points = patrolPoints;
            entityType = eType;
        }
    }
    /// <summary>
    /// Each humanoid requires pathfinding. This class is for customers and employees.
    /// @member m_location : The location in the game this humanoid is currently located.
    /// @member agent : The navmeshnagent component used for walking around the environment.
    /// @member patrolPoints: The points that the humanoid will be walking to and from.
    /// </summary>
    public class Humanoid : BaseEntity
    {
        public LocationType CurrentLocation;

        public PatrolPoints pPoints;

        protected FSM<Humanoid> StateMachine;

        public NavMeshAgent agent;

        // Use this for initialization
        protected void Start()
        {
            StateMachine = new FSM<Humanoid>();
            StateMachine.Owner = this;
            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.autoBraking = false;
            agent.speed = 50f;
            Debug.Log("Humanoid start finished");
            //StateMachine.SetCurrentState
        }

        // Update is called once per frame
        public void Update()
        {
            StateMachine.FSMUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FSM<Humanoid> GetFSM()
        {
            return StateMachine;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newLocation"></param>
        void SetCurrentLocation(LocationType newLocation)
        {
            CurrentLocation = newLocation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool HandleMessage(Telegram msg)
        {
            return StateMachine.HandleMessage(msg); 
        }
    }
}