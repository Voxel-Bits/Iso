using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{
    public class EatState :  State<Customer>
    {
        static EatState Instance = null;

        public void Awake()
        {
            Instance = this;
        }

        public static EatState GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// Set the patrol goal to be towards a nearby chair. If failed, just walk or stand and eat.
        /// </summary>
        /// <param name="entity"></param>
        public override void Enter(Customer entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Decrease the customer's hunger meter depending on the food they are currently 
        /// holding every x amount of seconds (depends on how quickly the customer can eat.
        /// </summary>
        /// <param name="entity"></param>
        public override void Execute(Customer entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
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
        public override bool OnMessaage(Customer entity, Telegram telegram)
        {
            throw new NotImplementedException();
        }
    }
}
