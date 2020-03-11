using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO
{
    public abstract class State : MonoBehaviour
    {
        protected StateMachine StateMachine;

        private String stateName;

        public String StateName
        {
            get => stateName;
            set => stateName = value;
        }

        protected virtual void Start()
        {
            this.StateMachine = GetComponent<StateMachine>();
        }

        public virtual void OnStateEnter()
        {
            this.enabled = true;
        }

        public virtual void Execute()
        {
        }

        public virtual void OnStateExit()
        {
            this.enabled = false;
        }
    }
}