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

        protected override void Awake()
        {
            base.Awake();
            OnStart();
        }

        protected override void OnStart()
        {
            StateName = "Idle";
            _dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
            _wander = GetComponent<Zombie_Wander>();
        }

        public override void Execute()
        {
            // Do nothing.
            // TODO: Wander around
            if (!_dataHolder.FlockManager.active)
            {
                this.ChangeState(_wander);
                _dataHolder.NavMeshAgent.SetDestination(Vector3.zero);
            }
        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is ZombieEvent humanEvent)) return;

            //TODO: Somehow get StateMachine to change state through a player controlled command
        }
    }
}