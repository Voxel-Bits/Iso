using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{
    public enum MoodType: long
    {
        Happy, Sad, Angry, Jubilant, Furious,Neutral
    }

    /// <summary>
    /// 
    /// </summary>
    public class Customer : Humanoid {

        public string DisplayName;
        public float Money;
        public float BathroomNeed;
        public float HungerNeed;
        public float FunMeter;
        public double TimeInPark;
        public string Thought;
        public MoodType CurrentMood;
        public static Transform[] PatrolPoints;

        // Use this for initialization
        void Start()
        {
            StateMachine.SetCurrentState(PatrolState.GetInstance());
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newHungerAmount"></param>
        public void SetHungerNeed(float newHungerAmount)
        {
            HungerNeed = newHungerAmount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseHunger(float amount)
        {
            HungerNeed += amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseHunger(float amount)
        {
            HungerNeed -= amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newBathroomAmount"></param>
        public void SetBathroomNeed(float newBathroomAmount)
        {
            BathroomNeed = newBathroomAmount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseBathroom(float amount)
        {
            BathroomNeed += amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseBathroom(float amount)
        {
            BathroomNeed -= amount;
        }

        public void SetFunMeter(float newFunAmount)
        {
            FunMeter = newFunAmount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseFun(float amount)
        {
            FunMeter += amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseFun(float amount)
        {
            FunMeter -= amount;
        }

        public void SetCurrentMood(MoodType newMood)
        {
            CurrentMood = newMood;
        }

        public void SetDisplayName(string newName)
        {
            DisplayName = newName;
        }
    }
}
