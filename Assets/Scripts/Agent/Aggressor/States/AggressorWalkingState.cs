using System;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AggressorWalkingState : State
    {
        private State _runningState;
        public AggressorDataHolder DataHolder;
        private Vector3 currentTarget;

        protected override void Start()
        {
            StateName = "Walking";
            //_runningState = GetComponent<RunningState>();
            DataHolder = (StateMachine as AggressorFsm).dataHolder;
            base.Start();
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

            if (DataHolder.move_target == null && DataHolder.defend_target == null)
            {
                StateMachine.ResetToDefaultState();
                return;
            }

            currentTarget = DataHolder.defend_target != null ? DataHolder.defend_target.Value : DataHolder.move_target.Value;
            if (transform.position != currentTarget)
            {
                // Move to target
                // Naive approach don't do it this way.
                transform.position =
                    Vector3.MoveTowards(transform.position, currentTarget, 1f * Time.deltaTime);
            }
            else
            {
                // Reach destination
                StateMachine.ResetToDefaultState();
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    this.ChangeState(_runningState);
            //}
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
                    DataHolder.move_target = new Vector3(hit.point.x, .5f, hit.point.z);
                    return true;
                }
            }

            return false;
        }
    }
}