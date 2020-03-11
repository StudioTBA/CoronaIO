using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace Com.StudioTBD.CoronaIO
{
    public abstract class StateMachine : MonoBehaviour
    {
        /// <summary>
        /// Default state if needed. Might be null.
        /// </summary>
        public State defaultState;

        /// <summary>
        /// State we are as of right now. Might be null. 
        /// </summary>
        public State currentState;

        /// <summary>
        /// On every start, any attached state class will be disabled except for the starting state.
        /// <b>Note:</b> Any children must call this method.
        /// </summary>
        protected virtual void Start()
        {
            foreach (State state in GetComponents<State>())
            {
                if (state == defaultState) continue;
                state.enabled = false;
            }
        }

        /// <summary>
        /// Method to transition from state to state.
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(State newState)
        {
            Debug.Log("Changing state: " + (currentState == null ? "Null" : currentState.GetType().Name) +
                      " - New State: " + (newState == null ? "Null" : newState.GetType().Name));

            if (currentState != null)
            {
                currentState.OnStateExit();
            }
            
            currentState = newState;

            if (currentState != null)
            {
                currentState.OnStateEnter();
            }
        }

        public void Update()
        {
            if (currentState != null && currentState.enabled)
            {
                currentState.Execute();
            }
        }

        public void ResetToDefaultState()
        {
            ChangeState(defaultState);
        }
    }
}