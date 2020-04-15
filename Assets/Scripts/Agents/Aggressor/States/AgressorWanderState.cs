using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AgressorWanderState : State
    {
        private State _runningState;
        private State _attackingState;
        private State _attackandretreat;
        public AggressorDataHolder DataHolder;
        private Vector3 currentTarget;


        protected override string SetStateName()
        {
            return "AgressorWanderState";
        }

        protected override void OnStart()
        {
            _attackandretreat = GetComponent<AttackAndRetreatState>();
            _attackingState = GetComponent<AttackingState>();
            DataHolder = (StateMachine as AggressorFsm).dataHolder;
        }

        public override void OnStateEnter()
        {
            DataHolder = (StateMachine as AggressorFsm)?.dataHolder;
            InvokeRepeating("WanderToNewPosition", 0f, 2f);
            base.OnStateEnter();
        }

        public override void OnStateExit()
        {
            CancelInvoke();
            base.OnStateExit();
        }

        public override void Execute()
        {

            //stop animation if velocity is zero
            if (DataHolder.NavMeshAgent.remainingDistance <= DataHolder.NavMeshAgent.stoppingDistance)
            {
                if (!DataHolder.NavMeshAgent.hasPath || DataHolder.NavMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    DataHolder.Animator.SetBool("Walking", false);
                }
            }
            else
                DataHolder.Animator.SetBool("Walking", true);

            if (DataHolder.EnemyPosition != null)
            {
                if (Vector3.Distance(transform.position, DataHolder.EnemyPosition.Value) <= DataHolder.weapon.Range)
                {
                    
                    this.ChangeState(_attackingState);
                }
            }
        }

        private void WanderToNewPosition()
        {
            Vector3 newPos = RandomPoint(transform.position, 200f, -1);
            this.DataHolder.NavMeshAgent.SetDestination(newPos);
        }

        private Vector3 RandomPoint(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

            randDirection += origin;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

            return navHit.position;
        }

        public override void Consume(Event.Event @event)
        {
            if (!(@event is HumanEvent humanEvent)) return;

            switch (humanEvent.EventType)
            {
                case HumanEvent.HumanEventType.SpottedZombie:
                    break;
                case HumanEvent.HumanEventType.PoliceAlert:
                    DataHolder.defend_target = @event.Producer.transform.position;
                    this.ChangeState(_attackandretreat);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


    }
}