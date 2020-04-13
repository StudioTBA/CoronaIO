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
        private State _idle;
        public float flee_distance_goal;

        protected override string SetStateName()
        {
            return "Flee";
        }
        protected override void OnStart()
        {
            // StateName = "Flee";
            _dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
            _idle = GetComponent<Idle_Zombie>();
        }

        public override void Execute()
        {

            if (this.CheckAndTransitionToArrive(_dataHolder))
                return;

            //Run away from human target
            //Need to return to idle state once far enough away
            if (!_dataHolder.Target || (_dataHolder.Target.transform.position - transform.position).magnitude > flee_distance_goal)
            {
                this.ChangeState(_idle);
            }
            else
            {
                _dataHolder.NavMeshAgent.SetDestination(PointAwayFromTarget());
            }

        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is ZombieEvent humanEvent)) return;

            //TODO: Somehow get StateMachine to change state through a player controlled command
        }

        private Vector3 PointAwayFromTarget()
        {
            Vector3 point;

            point = 2 * transform.position - _dataHolder.Target.transform.position;

            return point;
        }
    }
}