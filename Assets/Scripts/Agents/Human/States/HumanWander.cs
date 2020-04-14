using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using RandomUnity = UnityEngine.Random;
using RandomSystem = System.Random;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    public class HumanWander : State
    {
        private static RandomSystem _random = new RandomSystem();

        private DataHolder _dataHolder;
        private State _seekShelter;
        private State _arriveToAggressor;


        public override void OnStateEnter()
        {
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
            InvokeRepeating("WanderToNewPosition", 0f, 2f);
            base.OnStateEnter();
        }

        public override void OnStateExit()
        {
            CancelInvoke();
            base.OnStateExit();
        }

        protected override string SetStateName()
        {
            return "HumanWander";
        }

        protected override void OnStart()
        {
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
            _seekShelter = GetComponent<SeekShelter>();
            _arriveToAggressor = GetComponent<ArriveToAggressor>();
        }

        public override void Execute()
        {
            //stop animation if velocity is zero
            if (_dataHolder.NavMeshAgent.velocity == Vector3.zero)
                this._dataHolder.Animator.SetBool("Walking", false);
            else
                this._dataHolder.Animator.SetBool("Walking", true);
            // Do nothing.
            // TODO: Wander around
        }

        private void WanderToNewPosition()
        {
            Vector3 newPos = RandomPoint(transform.position, gameObject.transform.localScale.x*20.0f, -1);
            this._dataHolder.NavMeshAgent.SetDestination(newPos);
            
        }

        private Vector3 RandomPoint(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = RandomUnity.insideUnitSphere * dist;

            randDirection += origin;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

            return navHit.position;
        }

        /// <summary>
        /// At the moment of writing this code, there is a 50% chance of fleeing,
        /// 50% of anything else (i.e: arrive to officer).
        /// </summary>
        /// <returns>True if it must go flee, false otherwise</returns>
        private bool MustSeekShelter()
        {
            var nextDouble = _random.NextDouble();
            if (nextDouble < _dataHolder.BecomeAggressorProbability)
            {
                return true;
            }

            return false;
        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is HumanEvent humanEvent)) return;
            switch (humanEvent.EventType)
            {
                case HumanEvent.HumanEventType.PoliceAlert:
                    this._dataHolder.Target = @event.Producer;
                    if (MustSeekShelter())
                    {
                        this.ChangeState(_seekShelter);
                    }
                    else
                    {
                        this.ChangeState(_arriveToAggressor);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}