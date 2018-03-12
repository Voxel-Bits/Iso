using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{

    public class BathroomState : State<Customer> 
    {
        static BathroomState Instance = null;

        public void Awake()
        {
            Instance = this;
        }

        public static BathroomState GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public override void Enter(Customer entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Decrement the customer's bathroom need over a short amount of time.
        /// </summary>
        /// <param name="entity"></param>
        public override void Execute(Customer entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change state to Patrol (or whatever the previous state was?)
        /// </summary>
        /// <param name="entity"></param>
        public override void Exit(Customer entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="telegram"></param>
        /// <returns></returns>
        public override bool OnMessage(Customer entity, Telegram telegram)
        {
            throw new NotImplementedException();
        }
    }
}
