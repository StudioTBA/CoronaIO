using System;
using Com.StudioTBD.CoronaIO.Event;
using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine;


namespace Com.StudioTBD.CoronaIO.Agent
{
    public abstract class Agent : MonoBehaviour, IStateMachineListener, EventBroker
    {
        public StateMachine stateMachine;
        public State defaultState;
        protected int maxHealth;
        protected int currentHealth;

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

        /// <summary>
        /// Override as needed.
        /// By default the state machine will redirect the event to each state.
        /// </summary>
        /// <param name="event">An event</param>
        public virtual void Consume(Event.Event @event)
        {
            if (stateMachine != null && stateMachine.CurrentState != null)
                stateMachine.CurrentState.Consume(@event);
        }
    }
}