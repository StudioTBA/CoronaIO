using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.Agent;
using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie
{
    public class ZombieAgent : Agent
    {
        private ZombieDataHolder _dataHolder = new ZombieDataHolder();
        private NavMeshAgent _navMeshAgent;

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _dataHolder.NavMeshAgent = _navMeshAgent;
            stateMachine = new ZombieStateMachine(_dataHolder);
            stateMachine.Setup(gameObject, defaultState, this);
        }

        public override void Consume(Event.Event @event)
        {
            if (@event is ZombieEvent)
            {
                base.Consume(@event);
            }
        }

        public override void OnStateEnter(State newState)
        {
        }

        public override void OnStateChange(State oldState, State newState)
        {
            Debug.Log($"{oldState?.StateName} - {newState?.StateName}", this);
        }

        public override void OnStateExit(State oldState)
        {
        }
    }
}