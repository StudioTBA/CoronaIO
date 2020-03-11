using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO
{
    public class ExampleRunningState : State
    {
        private State _idleState;
        
        protected override void Start()
        {
            base.Start();
            StateName = "Running";
            _idleState = GetComponent<ExampleIdleState>();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering " + this.GetType().FullName);
        }

        /// <summary>
        /// Example Execute function that transitions from Moving to Running.
        /// </summary>
        public override void Execute()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            { 
                Debug.Log(this.GetType().Name + ": Space pressed");
                StateMachine.ChangeState(_idleState);
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting " + this.GetType().FullName);
        }
    }
}