using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;



namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class DefendingState : State
    {
        private State _attack;
        public AggressorDataHolder DataHolder;

        protected override void Start()
        {
            base.Start();
            StateName = "Defending";
            _attack = GetComponent<AttackingState>();

        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering " + this.GetType().FullName);

            DataHolder = DataHolder == null ? (_stateMachine as AggressorFsm).dataHolder : DataHolder;


        }

        public override void Execute()
        {
                     


        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting " + this.GetType().FullName);
            //StopAllCoroutines();
        }

    }




}


