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
        public GameObject arriveParticleFXPrefab;
        private GameObject currentArriveParticleFX;

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _dataHolder.NavMeshAgent = _navMeshAgent;
            _dataHolder.FlockManager = GetComponent<FlockManager>();
            _dataHolder.arriveParticleFXPrefab = arriveParticleFXPrefab;
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
            if (newState.StateName == "Attack")
                notifyHorde("Attacking", true);
        }

        public override void OnStateChange(State oldState, State newState)
        {
            

            Debug.Log($"{oldState?.StateName} - {newState?.StateName}", this);
        }

        public override void OnStateExit(State oldState)
        {

            if (oldState.name == "Zombie_Attack")
                notifyHorde("Attack", false);
        }

        private void notifyHorde(string animation, bool isplaying)
        {
            foreach(Flocker Zombie in _dataHolder.FlockManager.getZombieList())
            {
                Zombie.animator.SetBool(animation, isplaying);
            }
           
        }


    }
}