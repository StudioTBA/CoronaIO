using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO
{
    public class ExampleIdleState : State
    {
        private ExampleMovingState _exampleMovingState;

        protected override void Start()
        {
            base.Start();
            StateName = "Idle";
            _exampleMovingState = GetComponent<ExampleMovingState>();
            Debug.Log(_exampleMovingState);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering " + this.GetType().FullName);
        }

        public override void Execute()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(this.GetType().Name + ": Space pressed");
                StateMachine.ChangeState(_exampleMovingState);
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting " + this.GetType().FullName);
        }
    }
}