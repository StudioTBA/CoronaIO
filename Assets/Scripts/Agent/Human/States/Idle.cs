using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using JetBrains.Annotations;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    public class Idle : State
    {
        private DataHolder _dataHolder;
        private State _movingState;
        private State _fleeState;

        protected override void Start()
        {
            base.Start();
            StateName = "Idle";
            _dataHolder = (StateMachine as HumanStateMachine).DataHolder;
            _movingState = GetComponent<Moving>();
            _fleeState = GetComponent<Flee>();
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

        public override void Consume([NotNull] Event.Event @event)
        {
            Debug.Log("Consuming event: " + @event, this);

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

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Idle - Enter", this);
        }
    }
}