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
        public GameObject PolicePrefab;
        public float BecomeAggressorProbability;
        public float SeekShelterRange;
        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _dataHolder.NavMeshAgent = _navMeshAgent;

            _dataHolder.PolicePrefab = PolicePrefab;
            _dataHolder.BecomeAggressorProbability = BecomeAggressorProbability;


            _dataHolder.Animator = GetComponentInChildren<Animator>();
            _dataHolder.rigidbody = GetComponent<Rigidbody>();


            _dataHolder.SeekShelterRange = SeekShelterRange;

            stateMachine = new HumanStateMachine(_dataHolder);
            stateMachine.Setup(gameObject, defaultState, this);
        }

        public override void Consume(Event.Event @event)
        {
            HumanEvent humanEvent = @event as HumanEvent;
            if (humanEvent != null)
            {
                if (humanEvent.EventType == HumanEvent.HumanEventType.PoliceAlert)
                {
                    Debug.Log("Police just alerted us");
                }

                base.Consume(@event);
            }
        }
    }
}