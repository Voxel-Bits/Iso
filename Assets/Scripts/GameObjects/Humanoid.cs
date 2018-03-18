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

        public NavMeshPath CurrentPath;

        public ConeCollider coneCollider;

        public Transform eyes;
        public float lookRange = 20f;
        public float lookSphereCastRadius = 1f;

        public Dictionary<int, InteractableEnvironmentObjects> CurrentNearbyIEObjects;

        // Use this for initialization
        protected void Start()
        {
            StateMachine = new FSM<Humanoid>();
            StateMachine.Owner = this;
            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.autoBraking = false;
            agent.speed = 50f;
            agent.velocity = Vector3.one;
            agent.angularSpeed = 2f;
            agent.acceleration = 5f;
            agent.updateRotation = false;

            //coneCollider = GetComponent<ConeCollider>();
           // DisableCollider();
            CurrentNearbyIEObjects = new Dictionary<int, InteractableEnvironmentObjects>();
        }

        // Update is called once per frame
        public void Update()
        {
            StateMachine.FSMUpdate();
        }

        /// <summary>
        /// Will clearing the dictionary not call the triggers the next frame?
        /// </summary>
        public void LateUpdate()
        {
           // Debug.Log("Clearing dictionary");
           // CurrentNearbyIEObjects.Clear();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        //private void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log("TriggerEnter \"" + other.gameObject.name + "\"Object");
        //    InteractableEnvironmentObjects ieObject = other.gameObject.GetComponent<InteractableEnvironmentObjects>();
        //    if (ieObject != null)
        //    {
        //        Debug.Log("Adding object " + ieObject.name);
        //        CurrentNearbyIEObjects.Add(ieObject.GetInstanceID(), ieObject);
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="other"></param>
        //private void OnTriggerExit(Collider other)
        //{
        //    InteractableEnvironmentObjects ieObject = other.gameObject.GetComponent<InteractableEnvironmentObjects>();
        //    if (ieObject != null)
        //    {
        //        Debug.Log("Removing object " + ieObject.name);
        //        CurrentNearbyIEObjects.Remove(ieObject.GetInstanceID());

        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        public void DisableCollider()
        {
            coneCollider.enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void EnableCollider()
        {
            coneCollider.enabled = true;
        }
    }
}