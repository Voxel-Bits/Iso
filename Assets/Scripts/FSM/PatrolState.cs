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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Humanoid to move in the world space. Walking animation to loop.
        /// Go through the patrol points and randomly go to one
        /// </summary>
        /// <param name="entity"></param>
        public override void Execute(Humanoid entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public override void Exit(Humanoid entity)
        {
            throw new NotImplementedException();
        }

        // Use this for initialization
        void Awake()
        {
            Instance = this;
        }


    }
}
