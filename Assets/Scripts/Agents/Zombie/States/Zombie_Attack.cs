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
        private State _wander;

        protected override string SetStateName()
        {
            return "Attack";
        }

        protected override void OnStart()
        {
            // StateName = "Attack";
            _dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
            _wander = GetComponent<Zombie_Wander>();
        }

        public override void Execute()
        {
            //Use Navmesh to head towards Human target
            if (_dataHolder.Target && !_dataHolder.FlockManager.always_flee)
                _dataHolder.NavMeshAgent.SetDestination(_dataHolder.Target.transform.position);
            else
            {
                this.ChangeState(_wander);
            }

            this.CheckAndTransitionToArrive(this, _dataHolder);

        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is ZombieEvent zombieEvent)) return;

            //TODO: Somehow get StateMachine to change state through a player controlled command
        }
    }
}