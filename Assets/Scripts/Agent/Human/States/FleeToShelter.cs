using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;

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

        protected override void Start()
        {
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
            base.Start();
        }

        private bool IsMoving = false;

        public override void Execute()
        {
            if (_dataHolder.Target == null) return;
            if (IsMoving) return;
            IsMoving = true;
            // GameObject _target = _dataHolder.Target;
            // Debug.Log("Moving towards Shelter at: " +
            //           _target.GetComponent<Shelter>().floorMeshRenderer.transform.position);
            // _dataHolder.NavMeshAgent.SetDestination(_target.GetComponent<Shelter>().floorMeshRenderer
            //     .transform.position);
            
            _dataHolder.NavMeshAgent.SetDestination(new Vector3(0,0,0));
            Debug.Log("Moving towards : " + _dataHolder.NavMeshAgent.destination);
            // Debug.Log(_dataHolder.NavMeshAgent.destination);

            // Check radius
            // if (!WithinRadius(transform.position, _dataHolder.Target.transform.position))
            // {
            //     _stateMachine.ResetToDefaultState();
            //     return;
            // }
            // else
            // {
            // }
        }

        private bool WithinRadius(Vector3 currentPosition, Vector3 targetPosition)
        {
            return Vector3.Distance(currentPosition, targetPosition) > 50f;
        }
    }
}