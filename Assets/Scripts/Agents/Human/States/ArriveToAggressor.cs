using System;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    public class ArriveToAggressor : State
    {
        private DataHolder _dataHolder;
        private Vector3 _previousPosition;

        protected override string SetStateName()
        {
            return "ArriveToAggressor";
        }

        protected override void OnStart()
        {
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
        }

        public override void Execute()
        {
            if (_dataHolder.Target == null)
            {
                this.ResetToDefaultState();
                return;
            }

            var currentPos = _dataHolder.Target.transform.position;

            // Check if the previous position is the same, no need to set new destination.
            if (currentPos == _previousPosition)
            {
                return;
            }

            _previousPosition = currentPos;

            _dataHolder.NavMeshAgent.SetDestination(_previousPosition);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<PoliceAgent>())
            {
                var civilianPos = transform;
                var position = civilianPos.position;
                if (_dataHolder.PolicePrefab == null)
                {
                    Debug.Log("ERROR");
                    return;
                }

                var aggressor = Instantiate(_dataHolder.PolicePrefab, position, civilianPos.rotation);
                aggressor.transform.localScale = transform.localScale*5;
                aggressor.GetComponent<NavMeshAgent>().Warp(position);
                Destroy(this.gameObject);
            }
        }
    }
}