using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso
{
    public enum MoodType: long
    {
        Happy, Sad, Angry, Jubilant, Furious
    }

    /// <summary>
    /// 
    /// </summary>
    public class Customer : Humanoid {

        public float Money;
        public float BathroomNeed;
        public float HungerNeed;
        public float FunMeter;
        public double TimeInPark;
        public string Thought;
        public MoodType Mood;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
