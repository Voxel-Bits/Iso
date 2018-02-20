using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditor;

namespace Iso
{
    public abstract class State : MonoBehaviour {

        public abstract void Enter(Level_Object Lvl_Obj);

        public abstract void Execute(Level_Object Lvl_Obj);

        public abstract void Exit(Level_Object Lvl_Obj);

    }
}
