using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.StudioTBD.CoronaIO.FMS.Example;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class PoliceAgent : MonoBehaviour
    {
        public Camera _camera;
        public StateMachine stateMachine;
        public State _defaultState;
        public Text stateText;
        [SerializeField] private LayerMask enemylayer;
        private DataHolder _dataHolder = new AggressorDataHolder();


        private void Awake()
        {
            stateMachine = new AgentFsm(_dataHolder);
            stateMachine.Setup(gameObject, _defaultState);
        }

        public void Start()
        {
            stateMachine.Start();
            (stateMachine as AggressorFsm).DataHolder.enemyLayer = enemylayer;
        }


        // Update is called once per frame
        void Update()
        {
            stateMachine.Execute();

            if (stateMachine.CurrentState != null)
            {
                stateText.text = "State: " + stateMachine.CurrentState.StateName;
            }
            else
                stateText.text = "State: Null";
        }
    }
}


