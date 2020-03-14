using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Com.StudioTBD.CoronaIO.FMS.Example
{
    /// <summary>
    /// Example of an entity that uses a FiniteStateMachine. 
    /// </summary>
    public class Agent : MonoBehaviour
    {
        public Camera _camera;
        public StateMachine stateMachine;
        public State _defaultState;
        public Text stateText;

        private DataHolder _dataHolder = new DataHolder();
        

        private void Awake()
        {
            stateMachine = new AgentFsm(_dataHolder);
            stateMachine.Setup(gameObject, _defaultState);
        }
        
        public void Start(){
            stateMachine.Start();
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