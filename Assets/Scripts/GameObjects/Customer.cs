using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Iso
{
    public enum MoodType
    {
        Happy, Sad, Angry, Jubilant, Furious,Neutral
    };

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
        public MoodType CurrentMood = MoodType.Neutral;
        public static Transform[] patrolPoints;
        public InteractableEnvironmentObjects ObjUsing;
        public Vector3 currentDestination;

        

        // Use this for initialization
        void Start()
        {
            
            Debug.Assert(GridBase.GetInstance() != null, "Customer:: Start: Sorry!!!! GridBase not initialized yet.");
            patrolPoints = GridBase.GetInstance().startNavPoints;
            pPoints = new PatrolPoints(patrolPoints, EntityType.Customer);

            base.Start();
            Debug.Assert(StateMachine != null, "Customer:: Start: Sorry!!! Apparently derived class gets initialized before base class.");
            StateMachine.SetCurrentState(IdleState.GetInstance());
        }


        /// <summary>
        /// TODO: change it so it's not changing the current destination each frame
        /// </summary>
        public void Update()
        {
            base.Update();
            currentDestination = agent.destination;
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
