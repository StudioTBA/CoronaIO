using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.Agent.Example.States
{
    public class Flee : State
    {
        private DataHolder DataHolder;
        private State _idleState;
        private State _arriveState;

        protected override void Start()
        {
            base.Start();
            StateName = "Human - Idle";
            DataHolder = (StateMachine as HumanStateMachine).DataHolder;

            _idleState = GetComponent<Idle>();
            _arriveState = GetComponent<Arrive>();
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
                Vector3 oppositePosition = new Vector3(-DataHolder.Target.Value.x, DataHolder.Target.Value.y,
                    -DataHolder.Target.Value.z);
                transform.position =
                    Vector3.MoveTowards(transform.position, oppositePosition, 4f * Time.deltaTime);
            }
            else
            {
                // Reach destination
                StateMachine.ResetToDefaultState();
            }

            // Switch to arrive
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.ChangeState(_arriveState);
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