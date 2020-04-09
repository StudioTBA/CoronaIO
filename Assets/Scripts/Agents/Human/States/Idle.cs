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

        protected override string SetStateName()
        {
            return "Idle";
        }

        protected override void OnStart()
        {
            // StateName = "Idle";
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
            _seekShelter = GetComponent<SeekShelter>();
        }

        public override void Execute()
        {
            // Do nothing.
            // TODO: Wander around
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