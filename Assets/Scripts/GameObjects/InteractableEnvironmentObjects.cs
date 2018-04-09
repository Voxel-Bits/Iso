using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using LevelEditor;
using System;

namespace Iso
{
    public enum StatusType: long
    {
        BrokenDown, Open, Close, InUse, Idle
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
        private NavMeshModifier navModifier;
        public string DisplayName;
        public bool Deletable;
        public int PriceToUse;
        public int PriceToMaintain;
        LocationType CurrentLocation;
        public int WaitTime;
        public int Popularity;
        public int Rating;
        public int TimeToInteract; //this is different then wait time because an object can have a long ass line, but only take 5 seconds to play.
        public int MaxNumOfHumanoids;
        public int CurrNumOfHumanoids;

        StatusType CurrentStatus;

        /// <summary>
        /// Set the display name of the interactable object.
        /// </summary>
        /// <param name="name"></param>
        public void SetDisplayName(string name)
        {
            DisplayName = name;
        }

        /// <summary>
        /// Set if the player can delete the interactable object.
        /// </summary>
        /// <param name="deletable"></param>
        public void SetIfDeletable(bool deletable)
        {
            Deletable = deletable;
        }

        /// <summary>
        /// Set the wait time of the interactable object. (How long does a customer need to wait in order to use it?)
        /// </summary>
        /// <param name="newWait"></param>
        public void SetWaitTime(int newWait)
        {
            WaitTime = newWait;
        }

        /// <summary>
        /// Increase the wait time of the interactable object. 
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseWaitTime(int amount)
        {
            WaitTime += amount;
        }

        /// <summary>
        /// Decrease the wait time of the interactable object.
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseWaitTime(int amount)
        {
            Debug.Assert(WaitTime > 0, "InteractableEnvironmentObjects::DecreaseWaitTime : WaitTime is already zero");
            Debug.Assert(WaitTime - amount > 0, "InteractableEnvironmentObjects::DecreaseWaitTime : The amount you're trying to decrease the wait time to will make it a negative number");
            WaitTime -= amount;
        }

        /// <summary>
        /// Set the price required to use the interactable object.
        /// </summary>
        /// <param name="newPrice"></param>
        public void SetPriceToUSe(int newPrice)
        {
            PriceToUse = newPrice;
        }

        /// <summary>
        /// Increase the price required to use the interactable object.
        /// </summary>
        /// <param name="amount"></param>
        public void IncreasePriceToUse(int amount)
        {
            PriceToUse += amount;
        }

        /// <summary>
        /// Decrease the price to use the interactable object.
        /// </summary>
        /// <param name="amount"></param>
        public void DecreasePriceToUse(int amount)
        {
            PriceToUse -= amount;
            Debug.Assert(PriceToUse >= 0, "InteractableEnvironmentObjects::DecreasePriceToPlay : You can't have a negative amount to play. You can't GIVE people money to play.");
        }

        /// <summary>
        /// Set the price to maintain the interactable object.
        /// </summary>
        /// <param name="newPrice"></param>
        public void SetPriceToMaintain(int newPrice)
        {
            PriceToMaintain = newPrice;
        }

        /// <summary>
        /// Increase the price to maintain the interactable object.
        /// </summary>
        /// <param name="amount"></param>
        public void IncreasePriceToMaintain(int amount)
        {
            PriceToMaintain += amount;
        }

        /// <summary>
        /// Decrease the price to maintain the interactable object.
        /// </summary>
        /// <param name="amount"></param>
        public void DecreasePriceToMaintain(int amount)
        {
            PriceToMaintain -= amount;
            Debug.Assert(PriceToMaintain >= 0, "InteractableEnvironmentObject::DecreasePriceToMaintain : You can't have a negative amount to maintain.");
        }

        /// <summary>
        /// Set how popular the interactable object is.
        /// </summary>
        /// <param name="amount"></param>
        public void SetPopularity(int amount)
        {
            Popularity = amount;
        }

        /// <summary>
        /// Increase the popularity of the interactable object.
        /// </summary>
        /// <param name="amount"></param>
        public void IncreasePopularity(int amount)
        {
            Popularity += amount;
        }

        /// <summary>
        /// Decrease the popularity of the interactable object.
        /// </summary>
        /// <param name="amount"></param>
        public void DecreasePopularity(int amount)
        {
            Popularity -= amount;
        }

        /// <summary>
        /// Set the current location of the interactable object.
        /// </summary>
        /// <param name="newLocation"></param>
        public void SetCurrentLocation(LocationType newLocation)
        {
            CurrentLocation = newLocation;
        }

        /// <summary>
        /// Set the current status of the interactable object. (Is it broken, closed, open, idle?)
        /// </summary>
        /// <param name="newStatus"></param>
        public void SetCurrentStatus(StatusType newStatus)
        {
            CurrentStatus = newStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool HandleMessage(Telegram msg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetAreaType()
        {

        }
    }
}
