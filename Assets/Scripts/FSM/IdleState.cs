using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Iso
{

    /// <summary>
    /// No moving, just running animations
    /// </summary>
    public class IdleState : State<Humanoid>
    {
        private static IdleState Instance = null;

        public static IdleState GetInstance()
        {
            return Instance;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public override void Enter(Humanoid entity)
        {
            Debug.Log("In Idle State Enter");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public override void Execute(Humanoid entity)
        {
            //entity.GetFSM().ChangeState(PatrolState.GetInstance());
            Debug.Log("In Idle State Execute");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public override void Exit(Humanoid entity)
        {
            Debug.Log("In Idle State Exit");
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

        // Use this for initialization
        void Awake()
        {
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}