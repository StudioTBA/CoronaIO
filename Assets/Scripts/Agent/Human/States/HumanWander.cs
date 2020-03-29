using System;
using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    public class HumanWander : State
    {
        private DataHolder _dataHolder;
        private State _seekShelter;


        public override void OnStateEnter()
        {
            InvokeRepeating("WanderToNewPosition", 0f, 2f);
            base.OnStateEnter();
        }

        public override void OnStateExit()
        {
            CancelInvoke();
            base.OnStateExit();
        }

        protected override void Start()
        {
            StateName = "HumanWander";
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
            _seekShelter = GetComponent<SeekShelter>();
            base.Start();
        }

        public override void Execute()
        {
            // Do nothing.
            // TODO: Wander around
        }

        private bool isWandering = true;

        private void WanderToNewPosition()
        {
            Vector3 newPos = RandomPoint(transform.position, 200f, -1);
            Debug.Log("Wandering to new position: " + newPos);
            this._dataHolder.NavMeshAgent.SetDestination(newPos);
        }

        private Vector3 RandomPoint(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;

            randDirection += origin;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

            return navHit.position;
        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is HumanEvent humanEvent)) return;

            switch (humanEvent.EventType)
            {
                case HumanEvent.HumanEventType.SpottedZombie:
                    this._dataHolder.Target = @event.Producer;
                    this.ChangeState(_seekShelter);
                    break;
                case HumanEvent.HumanEventType.PoliceAlert:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}