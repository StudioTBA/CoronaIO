using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.FMS
{
    public abstract class State : MonoBehaviour, Event.EventHandler
    {
        /// <summary>
        /// State machine to which this state belongs.
        /// </summary>
        protected StateMachine _stateMachine;

        private String _stateName;

        /// <summary>
        /// Name of the state for debugging purposes.
        /// <b>Note:</b> Must be set before being used.
        /// </summary>
        public String StateName
        {
            get => _stateName;
            set => _stateName = value;
        }

        private void Awake()
        {
        }

        public void Setup(StateMachine stateMachine)
        {
            this._stateMachine = stateMachine;
        }

        /// <summary>
        /// Note: Should be called as the last statement in the children's "Start"
        /// </summary>
        protected virtual void Start()
        {
            this.enabled = false;
        }
        
        /// <summary>
        /// Callback that is called when you enter the state.
        /// On entering the state, it is automatically enabled by calling base.OnStateEnter().
        /// This is to prevent unintended code execution.
        /// </summary>
        public virtual void OnStateEnter()
        {
            this.enabled = true;
        }

        /// <summary>
        /// Main function of the state. Called on every Update(). Must be overridden.
        /// </summary>
        public virtual void Execute()
        {
        }

        /// <summary>
        /// Callback that is called when you exit the state.
        /// On exiting the state, it is automatically disabled by calling base.OnStateEnter().
        /// This is to prevent unintended code execution.
        /// </summary>
        public virtual void OnStateExit()
        {
            this.enabled = false;
        }

        public StateMachine StateMachine
        {
            get { return _stateMachine; }
        }

        public virtual void Consume(Event.Event @event)
        {
            // Override if needed.
        }
    }
}