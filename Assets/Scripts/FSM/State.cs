using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditor;

namespace Iso
{

    /// <summary>
    /// Abstract class in order for all states to adhere to the same methods. 
    /// </summary>
    /// <typeparam name="entity_type"></typeparam>
    public abstract class State<entity_type> : MonoBehaviour {

        /// <summary>
        /// Actions that only need to be executed once and at the beginning of the state.
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Enter(entity_type entity);

        /// <summary>
        /// Actions that need to be looped (it will be called in an update function)
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Execute(entity_type entity);

        /// <summary>
        /// Actions that only need to be executed once and at the end of the state.
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Exit(entity_type entity);

    }
}
