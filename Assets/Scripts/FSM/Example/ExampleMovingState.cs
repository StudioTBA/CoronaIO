using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO
{
    public class ExampleMovingState : State
    {
        private ExampleIdleState _idleState;
        
        protected override void Start()
        {
            base.Start();
            StateName = "Moving";
            _idleState = GetComponent<ExampleIdleState>();
            Debug.Log(_idleState);
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