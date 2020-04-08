using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    /// <summary>
    /// Fake flee
    /// </summary>
    public class FleeToShelter : State
    {
        private DataHolder _dataHolder;

        protected override void Awake()
        {
            base.Awake();
            StateName = "Flee To Shelter";
        }

        protected override void OnStart()
        {
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
        }

        private bool IsMoving = false;

        public override void Execute()
        {
            if (_dataHolder.Target == null) return;
            if (IsMoving) return;
            IsMoving = true;

            GameObject target = _dataHolder.Target;
            NavMeshAgent agent = _dataHolder.NavMeshAgent;
            agent.ResetPath();
            _dataHolder.NavMeshAgent.SetDestination(target.GetComponent<Shelter>().floorMeshRenderer
                .transform.position);

            // Check radius if needed
            // if (!WithinRadius(transform.position, _dataHolder.Target.transform.position))
            // {
            //     _stateMachine.ResetToDefaultState();
            //     return;
            // }
            // else
            // {
            // }
        }
    }
}