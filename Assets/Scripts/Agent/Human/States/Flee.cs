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
        // private State _movingState;

        protected override void Start()
        {
            StateName = "Flee";
            _dataHolder = (StateMachine as HumanStateMachine).DataHolder;
            base.Start();
            // _movingState = GetComponent<Moving>();
        }


        public override void OnStateEnter()
        {
            base.OnStateEnter();
            _dataHolder.NavMeshAgent.velocity = Vector3.zero;
            _dataHolder.NavMeshAgent.SetDestination(new Vector3(0, 0.5f, 0));
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