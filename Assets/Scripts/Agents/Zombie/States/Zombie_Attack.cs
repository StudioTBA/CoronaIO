﻿using Com.StudioTBD.CoronaIO.FMS;
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

            if (this.CheckAndTransitionToArrive(_dataHolder))
                return;

            //Use Navmesh to head towards Human target
            if (_dataHolder.Target && !_dataHolder.FlockManager.always_flee)
            {
                Vector3 unitOffset = new Vector3(1, 0, 1);
                _dataHolder.NavMeshAgent.Warp(_dataHolder.Target.transform.position+unitOffset.normalized*5);
            }
            else
            {
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