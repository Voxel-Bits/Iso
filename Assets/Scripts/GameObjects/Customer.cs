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
        public InteractableEnvironmentObjects ObjUsing;

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
        /// Setting how hungry the customer is.
        /// </summary>
        /// <param name="newHungerAmount"></param>
        public void SetHungerNeed(float newHungerAmount)
        {
            HungerNeed = newHungerAmount;
        }

        /// <summary>
        /// Increasing how hungry the customer is.
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseHunger(float amount)
        {
            HungerNeed += amount;
        }

        /// <summary>
        /// Decreasing how hungry the customer is.
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseHunger(float amount)
        {
            HungerNeed -= amount;
        }

        /// <summary>
        /// Setting how much the customer needs to go to the bathroom.
        /// </summary>
        /// <param name="newBathroomAmount"></param>
        public void SetBathroomNeed(float newBathroomAmount)
        {
            BathroomNeed = newBathroomAmount;
        }

        /// <summary>
        /// Increasing how much the customer needs to go to the bathroom.
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseBathroom(float amount)
        {
            BathroomNeed += amount;
        }

        /// <summary>
        /// Decreasing how much the customer needs to go to the bathroom.
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseBathroom(float amount)
        {
            BathroomNeed -= amount;
        }

        /// <summary>
        /// Setting how much fun the customer is having.
        /// </summary>
        /// <param name="newFunAmount"></param>
        public void SetFunMeter(float newFunAmount)
        {
            FunMeter = newFunAmount;
        }

        /// <summary>
        /// Increasing the amount of fun the customer is having.
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseFun(float amount)
        {
            FunMeter += amount;
        }

        /// <summary>
        /// Decreasing the amount of fun the customer is having.
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseFun(float amount)
        {
            FunMeter -= amount;
        }

        /// <summary>
        /// Setting the current mood of the customer.
        /// </summary>
        /// <param name="newMood"></param>
        public void SetCurrentMood(MoodType newMood)
        {
            CurrentMood = newMood;
        }

        /// <summary>
        /// Setting the display name of the customer.
        /// </summary>
        /// <param name="newName"></param>
        public void SetDisplayName(string newName)
        {
            DisplayName = newName;
        }
    }
}
