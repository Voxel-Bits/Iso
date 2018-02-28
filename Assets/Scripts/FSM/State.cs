using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditor;

namespace Iso
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="entity_type"></typeparam>
    public abstract class State<entity_type> : MonoBehaviour {

        public abstract void Enter(entity_type entity);

        public abstract void Execute(entity_type entity);

        public abstract void Exit(entity_type entity);

    }
}
