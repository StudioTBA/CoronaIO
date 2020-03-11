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
        public State defaultState;
        public State currentState;

        protected virtual void Start()
        {
            foreach (State state in GetComponents<State>())
            {
                if (state == currentState) continue;
                state.enabled = false;
            }
        }

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
            if(currentState != null && currentState.enabled)
            {
                currentState.Execute();
            }
        }
    }
}