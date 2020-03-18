using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.Agent.Example.States
{
    public class Arrive : State
    {
        private DataHolder DataHolder;
        private State _fleeState;
        private State _idleState;

        protected override void Start()
        {
            base.Start();
            StateName = "Human - Arrive";
            DataHolder = (StateMachine as HumanStateMachine).DataHolder;

            _fleeState = GetComponent<Flee>();
            _idleState = GetComponent<Arrive>();
        }

        public override void Execute()
        {
            HandleMouseClick();

            if (DataHolder.Target == null)
            {
                StateMachine.ResetToDefaultState();
                return;
            }

            if (transform.position != DataHolder.Target)
            {
                // Move to target
                // Naive approach don't do it this way.
                transform.position =
                    Vector3.MoveTowards(transform.position, DataHolder.Target.Value, 4f * Time.deltaTime);
            }
            else
            {
                // Reach destination
                StateMachine.ResetToDefaultState();
            }

            // Switch to flee
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.ChangeState(_fleeState);
            }
        }

        private bool HandleMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    DataHolder.Target = new Vector3(hit.point.x, .5f, hit.point.z);
                    return true;
                }
            }

            return false;
        }
    }
}