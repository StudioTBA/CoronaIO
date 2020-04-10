using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie.States
{
    public class Idle_Zombie : State
    {
        private ZombieDataHolder _dataHolder;
        private State _wander;
        private State _seekClosestHuman;

        protected override void Awake()
        {
            base.Awake();
            OnStart();
        }


        protected override string SetStateName()
        {
            return "Idle_Zombie";
        }

        protected override void OnStart()
        {
            // StateName = "Idle";
            _dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
            _wander = GetComponent<Zombie_Wander>();
            _seekClosestHuman = GetComponent<SeekClosestHuman>();
        }

        public override void Execute()
        {
            if (!_dataHolder.FlockManager.stop)
            {
                if (_dataHolder.FlockManager.always_flee)
                    this.ChangeState(_seekClosestHuman);
                else
                    this.ChangeState(_wander);
            }
        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is ZombieEvent zombieEvent)) return;

            //TODO: Somehow get StateMachine to change state through a player controlled command
        }
    }
}