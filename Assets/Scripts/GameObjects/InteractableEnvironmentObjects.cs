using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using LevelEditor;

namespace Iso
{
    public enum StatusType: long
    {
        BrokenDown, Open, Close, InUse
    }

    /// <summary>
    /// This class is for:
    /// - Game Cabinet items
    /// - Ticket dispensing items
    /// - Pinball machine items
    /// - Prize table item
    /// - Vending machine items
    /// - etc
    /// THIS should be a serialized class!
    /// </summary>
    public class InteractableEnvironmentObjects : BaseEntity {

        public GameObject PlayArea;
        public GameObject WatchArea;
        public NavMeshObstacle navObstacle;
        public string DisplayName;
        public bool Deletable;
        public int PriceToPay;
        public int PriceToMaintain;
        LocationType Location;
        public int WaitTime;
        public int Popularity;
        public int Rating;
        StatusType Status;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}
