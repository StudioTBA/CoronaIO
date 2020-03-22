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
        }
    }
}