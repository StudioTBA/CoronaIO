using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using System;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AggressorIdleState : State
    {
        private State _walkingState;
        private State _attackingState;

        private AggressorDataHolder DataHolder;

        protected override string SetStateName()
        {
            return "Idle";
        }

        protected override void OnStart()
        {
            _walkingState = GetComponent<AggressorWalkingState>();
            _attackingState = GetComponent<AttackingState>();

            DataHolder = (StateMachine as AggressorFsm).dataHolder;
        }

        public override void Execute()
        {
            if (DataHolder.EnemyPosition != null)
            {
                if (Vector3.Distance(transform.position, DataHolder.EnemyPosition.Value) <= DataHolder.weapon.Range)
                {
                    this.ChangeState(_attackingState);
                }
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting " + this.GetType().FullName);
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
                    this.ChangeState(_walkingState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}