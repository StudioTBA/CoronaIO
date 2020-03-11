using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO
{
    public abstract class State : MonoBehaviour
    {
        /// <summary>
        /// State machine where that this state belongs to.
        /// </summary>
        protected StateMachine StateMachine;
        
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

        protected virtual void Start()
        {
            this.StateMachine = GetComponent<StateMachine>();
        }

        /// <summary>
        /// Callback when that is called when you enter the state.
        /// On entering the state, it is automatically enabled by calling base.OnStateEnter().
        /// This is to prevent unintended code execution.∑
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
        /// Callback when that is called when you exit the state.
        /// On exiting the state, it is automatically disabled by calling base.OnStateEnter().
        /// This is to prevent unintended code execution.
        /// </summary>
        public virtual void OnStateExit()
        {
            this.enabled = false;
        }
    }
}