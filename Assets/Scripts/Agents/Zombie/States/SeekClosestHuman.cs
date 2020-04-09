using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie.States
{
    public class SeekClosestHuman : State
    {
        private ZombieDataHolder _dataHolder;


        protected override string SetStateName()
        {
            return "SeekClosestHuman";
        }

        protected override void OnStart()
        {
            // StateName = "SeekClosestHuman";
            _dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
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