using System;
using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine;
using UnityEngine.Serialization;

namespace Com.StudioTBD.CoronaIO.Agent
{
    public abstract class Agent : MonoBehaviour, IStateMachineListener
    {
        public StateMachine stateMachine;
        public State defaultState;

        /// <summary>
        /// See Example.Agent for guidance.
        /// </summary>
        protected virtual void Awake()
        {
            // Must override this.
            // Initialize here your stateMachine
            // stateMachine = new aStateMachine();
            // stateMachine.Setup(gameObject, defaultState);
        }

        protected void Start()
        {
            stateMachine?.Start();
        }

        protected void Update()
        {
            stateMachine?.Execute();
        }

        public virtual void OnStateExit(State oldState)
        {
        }

        public virtual void OnStateEnter(State newState)
        {
        }

        public virtual void OnStateChange(State oldState, State newState)
        {
        }
    }
}