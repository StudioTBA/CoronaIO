using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO
{
    public class ExampleWalkingState : State
    {
        private State _runningState;
        
        protected override void Start()
        {
            base.Start();
            StateName = "Walking";
            _runningState = GetComponent<ExampleRunningState>();
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
                StateMachine.ChangeState(_runningState);
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting " + this.GetType().FullName);
        }
    }
}