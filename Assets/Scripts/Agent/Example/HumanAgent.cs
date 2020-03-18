using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.Agent.Example
{
    public class HumanAgent : Agent
    {
        private DataHolder _dataHolder = new DataHolder();

        protected override void Awake()
        {
            base.Awake();
            stateMachine = new HumanStateMachine(_dataHolder);
            stateMachine.Setup(gameObject, defaultState, this);
            // stateMachine.AddListener(this);
        }

        public override void OnStateExit(State oldState)
        {
            base.OnStateExit(oldState);
        }

        public override void OnStateEnter(State newState)
        {
            base.OnStateEnter(newState);
        }

        public override void OnStateChange(State oldState, State newState)
        {
            Debug.Log("Changing state: " + (oldState == null ? "Null" : oldState.GetType().Name) +
                      " - New State: " + (newState == null ? "Null" : newState.GetType().Name));
        }
        
    }
}