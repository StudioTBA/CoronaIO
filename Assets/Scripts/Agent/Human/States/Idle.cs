using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    public class Idle : State
    {
        private DataHolder _dataHolder;
        private State _seekShelter;

        protected override void Start()
        {
            StateName = "Idle";
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
            _seekShelter = GetComponent<SeekShelter>();
            base.Start();
        }

        public override void Execute()
        {
            // Do nothing.
            // TODO: Wander around
            
            // if (!Input.GetMouseButtonDown(0)) return;
            //
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;
            //
            // if (!Physics.Raycast(ray, out hit)) return;
            // Vector3 target = new Vector3(hit.point.x, 50f, hit.point.z);
            // _dataHolder.NavMeshAgent.SetDestination(target);
            // Debug.Log("Moving towards: " + _dataHolder.NavMeshAgent.destination);
        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is HumanEvent humanEvent)) return;

            switch (humanEvent.EventType)
            {
                case HumanEvent.HumanEventType.SpottedZombie:
                    this._dataHolder.Target = @event.Producer;
                    this.ChangeState(_seekShelter);
                    break;
                case HumanEvent.HumanEventType.PoliceAlert:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}