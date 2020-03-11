﻿using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO
{
    public class ExampleIdleState : State
    {
        private State _exampleRunningState;

        protected override void Start()
        {
            base.Start();
            StateName = "Idle";
            _exampleRunningState = GetComponent<ExampleWalkingState>();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering " + this.GetType().FullName);
        }

        /// <summary>
        /// Example Execute function that transitions from Idle to Moving.
        /// </summary>
        public override void Execute()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(this.GetType().Name + ": Space pressed");
                StateMachine.ChangeState(_exampleRunningState);
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting " + this.GetType().FullName);
        }
    }
}