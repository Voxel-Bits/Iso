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

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

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
