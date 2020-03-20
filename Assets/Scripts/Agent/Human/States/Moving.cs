using System;
using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    public class Moving : State
    {
        private DataHolder _dataHolder;

        protected override void Start()
        {
            base.Start();
            StateName = "Idle";
            _dataHolder = (StateMachine as HumanStateMachine).DataHolder;
        }

        public override void Execute()
        {
            switch (_dataHolder.NavMeshAgent.pathStatus)
            {
                case NavMeshPathStatus.PathComplete:
                case NavMeshPathStatus.PathInvalid:
                    if (_dataHolder.NavMeshAgent.remainingDistance <= 1f)
                    {
                        StateMachine.ResetToDefaultState();
                    }
                    break;

                case NavMeshPathStatus.PathPartial:
                    break;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 target = new Vector3(hit.point.x, 50f, hit.point.z);
                    _dataHolder.NavMeshAgent.SetDestination(target);
                }
            }
        }
    }
}