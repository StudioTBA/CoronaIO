using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.Example
{
    public class RunningState : State
    {
        private State _walkingState;
        public DataHolder DataHolder;

        protected override void Start()
        {
            base.Start();
            StateName = "Running";
            _walkingState = GetComponent<WalkingState>();
            DataHolder = (StateMachine as AgentFsm).DataHolder;
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
            HandleMouseClick();

            if (DataHolder.target == null)
            {
                StateMachine.ResetToDefaultState();
                return;
            }

            if (transform.position != DataHolder.target)
            {
                // Move to target
                // Naive approach don't do it this way.
                transform.position =
                    Vector3.MoveTowards(transform.position, DataHolder.target.Value, 3f * Time.deltaTime);
            }
            else
            {
                StateMachine.ResetToDefaultState();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                StateMachine.ChangeState(_walkingState);
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting " + this.GetType().FullName);
        }
        
        private bool HandleMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    DataHolder.target = new Vector3(hit.point.x, .5f, hit.point.z);
                    return true;
                }
            }
            return false;
        }
    }
}