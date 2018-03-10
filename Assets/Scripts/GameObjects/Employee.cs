using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{

    /// <summary>
    /// 
    /// </summary>
    public class Employee : Humanoid
    {
        public float Salary;
        public static Transform[] PatrolPoints;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newSalary"></param>
        public void SetSalary(float newSalary)
        {
            Salary = newSalary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseSalary(float amount)
        {
            Salary += amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseSalary(float amount)
        {
            Salary -= amount;
        }
    }
}
