using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.Agent.Example.States
{
    public class Idle : State
    {
        private DataHolder DataHolder;
        private State _fleeState;
        private State _arriveState;

        protected override void Start()
        {
            base.Start();
            StateName = "Human - Idle";
            DataHolder = (StateMachine as HumanStateMachine).DataHolder;

            _fleeState = GetComponent<Flee>();
            _arriveState = GetComponent<Arrive>();
        }

        public override void Execute()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    DataHolder.Target = new Vector3(hit.point.x, .5f, hit.point.z);
                    this.ChangeState(_arriveState);
                }
            }
        }
    }
}