using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using Com.StudioTBD.CoronaIO.Agent.Human;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie.States
{
    public class Flee : State
    {
        private ZombieDataHolder _dataHolder;

        protected override void Start()
        {
            StateName = "Flee";
            _dataHolder = (StateMachine as ZombieStateMachine)?.DataHolder;
            base.Start();
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