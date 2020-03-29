using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie.States
{
    public class Zombie_Attack : State
    {
        private ZombieDataHolder _dataHolder;

        protected override void Start()
        {
            StateName = "Attack";
            _dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
            base.Start();
        }

        public override void Execute()
        {
            //Use Navmesh to head towards Human target
        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is ZombieEvent humanEvent)) return;

            //TODO: Somehow get StateMachine to change state through a player controlled command
        }
    }
}