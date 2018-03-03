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
        public int PriceToPlay;
        public int PriceToMaintain;
        LocationType CurrentLocation;
        public int WaitTime;
        public int Popularity;
        public int Rating;
        public int TimeToInteract; //this is different then wait time because an object can have a long ass line, but only take 5 seconds to play.
        StatusType CurrentStatus;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void SetDisplayName(string name)
        {
            DisplayName = name;
        }

        public void SetIfDeletable(bool deletable)
        {
            Deletable = deletable;
        }

        public void IncreaseWaitTime(int amount)
        {
            WaitTime += amount;
        }

        public void DecreaseWaitTime(int amount)
        {
            Debug.Assert(WaitTime > 0, "InteractableEnvironmentObjects::DecreaseWaitTime : WaitTime is already zero");
            Debug.Assert(WaitTime - amount > 0, "InteractableEnvironmentObjects::DecreaseWaitTime : The amount you're trying to decrease the wait time to will make it a negative number");
            WaitTime -= amount;
        }

        public void SetPriceToPlay(int newPrice)
        {
            PriceToPlay = newPrice;
        }

        public void IncreasePriceToPlay(int amount)
        {
            PriceToPlay += amount;
        }

        public void DecreasePriceToPlay(int amount)
        {
            PriceToPlay -= amount;
            Debug.Assert(PriceToPlay >= 0, "InteractableEnvironmentObjects::DecreasePriceToPlay : You can't have a negative amount to play. You can't GIVE people money to play.");
        }

        public void SetPriceToMaintain(int newPrice)
        {
            PriceToMaintain = newPrice;
        }

        public void IncreasePriceToMaintain(int amount)
        {
            PriceToMaintain += amount;
        }

        public void DecreasePriceToMaintain(int amount)
        {
            PriceToMaintain -= amount;
            Debug.Assert(PriceToMaintain >= 0, "InteractableEnvironmentObject::DecreasePriceToMaintain : You can't have a negative amount to maintain.");
        }

        public void SetPopularity(int amount)
        {
            Popularity = amount;
        }

        public void IncreasePopularity(int amount)
        {
            Popularity += amount;
        }

        public void DecreasePopularity(int amount)
        {
            Popularity -= amount;
        }

        public void SetCurrentLocation(LocationType newLocation)
        {
            CurrentLocation = newLocation;
        }

        public void SetCurrentStatus(StatusType newStatus)
        {
            CurrentStatus = newStatus;
        }
    }
}
