using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{

    /// <summary>
    /// This class is literally so the FSM can have a state to call as soon as it's spawned. It really only calls Patrol State and that's it.
    /// Is that good design you ask? You tell me.
    /// </summary>
    public class InitialState : State<Humanoid>
    {
        private static InitialState Instance = null;

        public static InitialState GetInstance()
        {
            return Instance;
        }

        public override void Enter(Humanoid entity)
        {
            Debug.Log("In InitialState Enter");
        }

        public override void Execute(Humanoid entity)
        {
            Debug.Log("In InitialState Execute");
            entity.GetFSM().ChangeState(PatrolState.GetInstance());
        }

        public override void Exit(Humanoid entity)
        {
            Debug.Log("In InitialState Exit");
        }

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
