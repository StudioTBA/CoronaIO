using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace Com.StudioTBD.CoronaIO.FMS
{
    public abstract class StateMachine
    {
        /// <summary>
        /// Default state if needed. Might be null.
        /// </summary>
        public State DefaultState;

        /// <summary>
        /// State we are as of right now. Might be null. 
        /// </summary>
        public State CurrentState;
        
        /// <summary>
        /// Method to transition from state to state.
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(State newState)
        {
            Debug.Log("Changing state: " + (CurrentState == null ? "Null" : CurrentState.GetType().Name) +
                      " - New State: " + (newState == null ? "Null" : newState.GetType().Name));

            if (CurrentState != null)
            {
                CurrentState.OnStateExit();
            }

            CurrentState = newState;

            if (CurrentState != null)
            {
                CurrentState.OnStateEnter();
            }
        }

        /// <summary>
        /// TODO: Document
        /// On every start, any attached state class will be disabled except for the starting state.
        /// <b>Note:</b> Any children must call this method.
        /// </summary>
        public void Setup(GameObject parent, State defaultState)
        {
            this.DefaultState = defaultState;
            foreach (State state in parent.GetComponents<State>())
            {
                state.Setup(this);
            }
        }

        public void Start()
        {
            ChangeState(DefaultState);
        }

        public void Pause()
        {
            ChangeState(null);
        }

        public void Execute()
        {
            if (CurrentState != null && CurrentState.enabled)
            {
                CurrentState.Execute();
            }
        }

        public void ResetToDefaultState()
        {
            ChangeState(DefaultState);
        }
    }
}