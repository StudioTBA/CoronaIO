using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    public class Idle : State
    {
        private DataHolder _dataHolder;
        private State _movingState;

        protected override void Start()
        {
            base.Start();
            StateName = "Idle";
            _dataHolder = (StateMachine as HumanStateMachine).DataHolder;
            _movingState = GetComponent<Moving>();
        }

        public override void Execute()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 target = new Vector3(hit.point.x, 50f, hit.point.z);
                    _dataHolder.NavMeshAgent.SetDestination(target);
                    this.ChangeState(_movingState);
                }
            }
        }
    }
}