using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie.States
{
    public class Zombie_Flee : State
    {
        private ZombieDataHolder _dataHolder;

        protected override void OnStart()
        {
            StateName = "Flee";
            _dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
        }

        public override void Execute()
        {
            //Run away from human target
            //Need to return to idle state once far enough away
        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is ZombieEvent humanEvent)) return;

            //TODO: Somehow get StateMachine to change state through a player controlled command
        }
    }
}