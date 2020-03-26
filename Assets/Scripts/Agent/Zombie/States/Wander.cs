﻿using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using Com.StudioTBD.CoronaIO.Agent.Human;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie.States
{
    public class Wander : State
    {
        private DataHolder _dataHolder;

        protected override void Start()
        {
            StateName = "Wander";
            _dataHolder = (StateMachine as ZombieStateMachine)?.DataHolder;
            base.Start();
        }

        public override void Execute()
        {
            //Must get here from idle state only if not currently controlled by player
        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is ZombieEvent humanEvent)) return;

            //TODO: Somehow get StateMachine to change state through a player controlled command
        }
    }
}