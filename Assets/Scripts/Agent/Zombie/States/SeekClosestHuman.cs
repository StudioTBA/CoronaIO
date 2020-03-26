using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using Com.StudioTBD.CoronaIO.Agent.Human;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie.States
{
    public class SeekClosestHuman : State
    {
        private DataHolder _dataHolder;

        protected override void Start()
        {
            StateName = "SeekClosestHuman";
            _dataHolder = (StateMachine as ZombieStateMachine)?.DataHolder;
            base.Start();
        }

        public override void Execute()
        {
            //Set target to closest human
        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is ZombieEvent humanEvent)) return;

            //TODO: Somehow get StateMachine to change state through a player controlled command
        }
    }
}