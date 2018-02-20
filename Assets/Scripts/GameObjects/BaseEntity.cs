﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{
    enum LocationType: long
    {
        Bathroom,
        FoodCourt,
        TheFloor,
        EmployeesOnly,
        OutsidePark
    }
    /// <summary>
    /// The base entity of all game objects. They will inherit from Level_Object and this
    /// All game objects have:
    /// -worldpos (L_O)
    /// -gridpos(L_O)
    /// -3dmodel (L_O)
    /// -ID (L_0)
    /// -serialization 
    /// All game objects need to be:
    /// -spawnable
    /// -unspawnable
    /// </summary>
    public interface IBaseEntity
    {


    }
}