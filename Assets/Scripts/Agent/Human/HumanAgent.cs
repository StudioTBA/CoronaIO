using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.Agent;
using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.Agent.Human
{
    public class HumanAgent : Agent
    {
        private DataHolder _dataHolder = new DataHolder();
        private NavMeshAgent _navMeshAgent;

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _dataHolder.NavMeshAgent = _navMeshAgent;
            stateMachine = new HumanStateMachine(_dataHolder);
            stateMachine.Setup(gameObject, defaultState, this);
        }

        public override void Consume(Event.Event @event)
        {
            if (@event is HumanEvent)
            {
                base.Consume(@event);
            }
        }
    }
}