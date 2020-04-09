using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    /// <summary>
    /// Fake flee
    /// </summary>
    public class Flee : State
    {
        private DataHolder _dataHolder;

        protected override string SetStateName()
        {
            return "Flee";
        }

        protected override void Awake()
        {
            base.Awake();
        }
        
        protected override void OnStart()
        {
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
        }

        public override void Execute()
        {
            if (_dataHolder.Target == null) return;

            // Check radius
            if (!WithinRadius(transform.position, _dataHolder.Target.transform.position))
            {
                _stateMachine.ResetToDefaultState();
                return;
            }
            else
            {
                // Run away
                var position = transform.position;
                Vector3 dirToPlayer = position - _dataHolder.Target.transform.position;
                Vector3 newPosition = position + dirToPlayer;
                _dataHolder.NavMeshAgent.SetDestination(newPosition);
            }
        }

        private bool WithinRadius(Vector3 currentPosition, Vector3 targetPosition)
        {
            return Vector3.Distance(currentPosition, targetPosition) > 50f;
        }
    }
}