using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    public class Moving : State
    {
        private DataHolder _dataHolder;
        private State _fleeState;

        protected override void Start()
        {
            StateName = "Moving";
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
            _fleeState = GetComponent<Flee>();
            base.Start();
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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (!Input.GetMouseButtonDown(0)) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit)) return;

            Vector3 target = new Vector3(hit.point.x, 50f, hit.point.z);
            _dataHolder.NavMeshAgent.SetDestination(target);
        }

        public override void Consume(Event.Event @event)
        {
            if (!(@event is HumanEvent humanEvent)) return;

            switch (humanEvent.EventType)
            {
                case HumanEvent.HumanEventType.SpottedZombie:
                    this.ChangeState(_fleeState);
                    break;
                case HumanEvent.HumanEventType.PoliceAlert:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}