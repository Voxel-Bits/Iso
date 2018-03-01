using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{

    /// <summary>
    /// All humanoids will have the ability to patrol between given points.
    /// </summary>
    public class PatrolState : State<Humanoid>
    {

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
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
