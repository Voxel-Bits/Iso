using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Iso
{

    /// <summary>
    /// Finite State Machine class that encapsulates state related data and methods.
    /// Agents can own instance of this class and delegate management of current , global, and previous states.
    /// Previous state is a record of the last state the agent was in.
    /// Global state is the state logic that's called every time the FSM is updated, a priority state
    /// that reduces conditional checks for each state.
    /// </summary>
    /// <typeparam name="entity_type"></typeparam>
    public class FSM<entity_type> : MonoBehaviour
    {

        public entity_type Owner; //this is probably whatever entity this script is attached to.

        State<entity_type> CurrentState;

        State<entity_type> PreviousState;

        State<entity_type> GlobalState;

        // Use this for initialization
        void Start()
        {
            CurrentState = null;
            PreviousState = null;
            GlobalState = null;
        }

        /// <summary>
        /// Update the FSM. If global or current state exists, execute them.
        /// </summary>
        public void FSMUpdate()
        {
            if (GlobalState != null)
            {
                GlobalState.Execute(Owner);
            }

            if (CurrentState != null)
            {
                CurrentState.Execute(Owner);
            }
        }


        /// <summary>
        /// Sets the given state as the current state.
        /// </summary>
        /// <param name="s"></param>
        public void SetCurrentState(State<entity_type> s)
        {
            CurrentState = s;
        }

        /// <summary>
        /// Sets the given state as the global state.
        /// </summary>
        /// <param name="s"></param>
        public void SetGlobalState(State<entity_type> s)
        {
            GlobalState = s;
        }

        /// <summary>
        /// Sets the given state as the previous state.
        /// </summary>
        /// <param name="s"></param>
        public void SetPreviousState(State<entity_type> s)
        {
            PreviousState = s;
        }

        /// <summary>
        /// Change states by exiting that curr state, setting the current state as the previous state, and entering the new current state.
        /// </summary>
        /// <param name="NewState"></param>
        public void ChangeState(State<entity_type> NewState)
        {
            Assert.IsTrue(NewState, "<FSM::ChangeState>: trying to change to a null state");

            PreviousState = CurrentState;
            CurrentState.Exit(Owner);
            CurrentState = NewState;
            CurrentState.Enter(Owner);
        }

        /// <summary>
        /// Change the current state to the previous state.
        /// </summary>
        public void RevertToPreviousState()
        {
            ChangeState(PreviousState);
        }

        /// <summary>
        /// Return the current state.
        /// </summary>
        /// <returns></returns>
        public State<entity_type> GetCurrentState()
        {
            return CurrentState;
        }

        /// <summary>
        /// Return the global state.
        /// </summary>
        /// <returns></returns>
        public State<entity_type> GetGlobalState()
        {
            return GlobalState;
        }

        /// <summary>
        /// Return the previous state.
        /// </summary>
        /// <returns></returns>
        public State<entity_type> GetPreviousState()
        {
            return PreviousState;
        }

        /// <summary>
        /// Return true if the current state's type is equal to the type of the class
        /// passed as a parameter.
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public bool IsInState(State<entity_type> st)
        {
            return CurrentState.isEqual(st);
        }

    }
}
