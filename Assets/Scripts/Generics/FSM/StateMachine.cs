using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Com.StudioTBD.CoronaIO.FMS
{
    public abstract class StateMachine
    {
        /// <summary>
        /// State Listener to notify of anything
        /// </summary>
        // [CanBeNull]
        // public IStateMachineListener MachineListener => _machineListener;
        private List<IStateMachineListener> _stateMachineListeners = new List<IStateMachineListener>();

        /// <summary>
        /// Default state if needed. Might be null.
        /// </summary>
        public State DefaultState;

        /// <summary>
        /// State we are as of right now. Might be null. 
        /// </summary>
        [CanBeNull] public State CurrentState;

        /// <summary>
        /// Method to transition from state to state.
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(State newState)
        {
            NotifyStateChange(CurrentState, newState);

            if (CurrentState != null)
            {
                CurrentState.enabled = false;
                NotifyStateExit(CurrentState);
                CurrentState.OnStateExit();
            }

            CurrentState = newState;

            if (CurrentState != null)
            {
                CurrentState.enabled = true;
                NotifyStateEnter(CurrentState);
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

            State[] states = parent.GetComponents<State>();

            foreach (State state in states)
            {
                state.Setup(this);
            }

            foreach (var state in states)
            {
                if (state != defaultState) continue;
                state.enabled = true;
                return;
            }
        }

        public void Setup(GameObject parent, State defaultState, IStateMachineListener listener)
        {
            Setup(parent, defaultState);
            this.AddListener(listener);
        }

        public void Start()
        {
            ChangeState(DefaultState);
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

        private void NotifyStateChange(State oldState, State newState)
        {
            foreach (var listener in _stateMachineListeners)
            {
                listener.OnStateChange(oldState, newState);
            }
        }

        private void NotifyStateExit(State oldState)
        {
            foreach (var listener in _stateMachineListeners)
            {
                listener.OnStateExit(oldState);
            }
        }

        private void NotifyStateEnter(State newState)
        {
            foreach (var listener in _stateMachineListeners)
            {
                listener.OnStateEnter(newState);
            }
        }

        public void AddListener(IStateMachineListener listener)
        {
            _stateMachineListeners.Add(listener);
        }

        public void RemoveListener(IStateMachineListener listener)
        {
            _stateMachineListeners.Remove(listener);
        }
    }

    public interface IStateMachineListener
    {
        void OnStateExit(State oldState);
        void OnStateEnter(State newState);
        void OnStateChange(State oldState, State newState);
    }
}