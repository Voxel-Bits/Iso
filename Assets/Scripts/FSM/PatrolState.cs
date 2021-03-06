﻿using System;
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

        private const float rotSpeed = 20f;

        private static PatrolState Instance = null;

        private IEnumerator coroutine;

        private List<GameObject> ieobjs;

        private bool ShouldGoToNextState = false;

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
            entity.agent.destination = entity.pPoints.points[destPoint].position;
            entity.CurrentPath = entity.agent.path;
            entity.agent.isStopped = false;

            coroutine = CheckForDecision(entity);

        }

        /// <summary>
        /// Customer to move in the world space. Walking animation to loop.
        /// Go through the patrol points and randomly go to one. Then change to playing state.
        /// TODO: This will change to any other state in the future
        /// </summary>
        /// <param name="entity"></param>
        public override void Execute(Humanoid entity)
        {
            if (!ShouldGoToNextState)
            {
                StartCoroutine(coroutine);
            }
            else
            {

                entity.GetFSM().ChangeState(IdleState.GetInstance());
            }
           // CheckIfGoToNextDestination(entity);
            

        }

        /// <summary>
        /// What is even the call stack at this point?
        /// This should have a busy loop until the agent reaches the desination.
        /// </summary>
        /// <param name="entity"></param>
        public override void Exit(Humanoid entity)
        {
            Debug.Log("In Patrol State Exit");
            StopCoroutine(coroutine);
            entity.agent.isStopped = true;

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
            ieobjs = new List<GameObject>();
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

            InstantlyTurn(entity);
            destPoint = (destPoint + 1) % entity.pPoints.points.Length;
        }

        /// <summary>
        /// Instanctly rotates the entity to face its destination.
        /// TODO: Have it face the way point it's currently going to.
        /// </summary>
        /// <param name="entity"></param>
        private void InstantlyTurn(Humanoid entity)
        {
            Vector3 direction = (entity.agent.destination - entity.gameObject.transform.position).normalized;
            Quaternion qDir = Quaternion.LookRotation(direction);
            entity.gameObject.transform.rotation = Quaternion.Slerp(entity.gameObject.transform.rotation, qDir, Time.deltaTime * rotSpeed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator CheckForDecision(Humanoid entity)
        {
            for (;;)
            {
                if (!entity.agent.pathPending && entity.agent.remainingDistance < 0.5f)
                {
                    GoToNextPoint(entity);
                }
                else
                {
                    // if(UnityEngine.Random.value < 0.4f)
                    //{

                    //}
                    if(IeObjectInRange(entity))
                    {
                        GoToAnIeObject(entity);
                        if (!entity.agent.pathPending && entity.agent.remainingDistance < 0.8f)
                        {
                            ShouldGoToNextState = true;
                            entity.agent.isStopped = true;
                            entity.agent.ResetPath();
                            Debug.Log("Agent should be stopped");
                            break;
                        }

                        
                    }
                }


                //CastSphere(entity);
                yield return new WaitForSeconds(.5f);
            }
        }

        void CastSphere(Humanoid entity)
        {

            RaycastHit[] hits;
            Debug.DrawRay(entity.eyes.position, entity.eyes.forward.normalized * entity.lookRange, Color.red);
            hits = Physics.SphereCastAll(entity.eyes.position, entity.lookSphereCastRadius, entity.eyes.forward, entity.lookRange);
               
        }
        
        /// <summary>
        /// Check if there's anything interesting
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IeObjectInRange(Humanoid entity)
        {
            Collider[] hits;

            Debug.DrawRay(entity.eyes.position, entity.eyes.forward.normalized * entity.lookRange, Color.red);
            hits = Physics.OverlapSphere(entity.eyes.position, entity.lookSphereCastRadius);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].GetComponent<InteractableEnvironmentObjects>() != null)
                {
                    return true;
                }

            }

            return false;
        }
        /// <summary>
        /// get the ieobjs and add to the list.
        /// If there's more than one ieobjs in list, determine which one is less popular.
        /// Though for now just go to the first one on the list
        /// </summary>
        /// <param name="entity"></param>
        void GoToAnIeObject(Humanoid entity)
        {
            Collider[] hits;

            Debug.DrawRay(entity.eyes.position, entity.eyes.forward.normalized * entity.lookRange, Color.red);
            hits = Physics.OverlapSphere(entity.eyes.position, entity.lookSphereCastRadius);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].GetComponent<InteractableEnvironmentObjects>() != null)
                {
                    ieobjs.Add(hits[i].transform.parent.gameObject);
                }
            }

            if (ieobjs.Count > 1)
            {
                entity.agent.isStopped = true;
                entity.agent.SetDestination(ieobjs[1].transform.Find("Player1").position);//MAGIC NUMBERRr
                InstantlyTurn(entity);
                entity.agent.isStopped = false;
            }
        }

        void CheckIfGoToNextDestination(Humanoid entity)
        {
            if (!entity.agent.pathPending && entity.agent.remainingDistance < 0.5f)
            {
                GoToNextPoint(entity);
            }
        }

    }
}

