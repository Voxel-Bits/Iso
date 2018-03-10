using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{
    public class PlayState : State<Customer>
    {
        static PlayState Instance = null;

        public void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static PlayState GetInstance()
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
        /// 
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
