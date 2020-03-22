using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    public class Flee : State
    {
        private DataHolder _dataHolder;
        // private State _movingState;

        protected override void Start()
        {
            base.Start();
            StateName = "Flee";
            _dataHolder = (StateMachine as HumanStateMachine).DataHolder;
            // _movingState = GetComponent<Moving>();
        }


        // public override void OnStateEnter()
        // {
        //     base.OnStateEnter();
        //     // _dataHolder.NavMeshAgent.isStopped = false;
        //     // _dataHolder.NavMeshAgent.SetDestination(new Vector3(0, 0.5f, 0));
        // }

        public override void Execute()
        {
        }


        public override void Consume(Event.Event @event)
        {
            Debug.Log("Consuming event: " + @event, this);

            Debug.Log("Consuming while Fleeing");
        }
    }
}