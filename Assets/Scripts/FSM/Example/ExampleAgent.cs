using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Com.StudioTBD.CoronaIO
{
    /// <summary>
    /// Example of an entity that uses a FiniteStateMachine. 
    /// </summary>
    public class ExampleAgent : MonoBehaviour
    {
        public Camera _camera;
        public StateMachine stateMachine;
        public Text stateText;
        
        // Update is called once per frame
        void Update()
        {
            if (stateMachine.currentState != null)
            {
                stateText.text = "State: " + stateMachine.currentState.StateName;
            }
            else
                stateText.text = "State: Null";
        }
        
    }
}