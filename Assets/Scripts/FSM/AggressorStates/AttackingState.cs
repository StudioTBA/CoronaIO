﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.FMS.Example;
using Com.StudioTBD.CoronaIO.FMS.Extensions;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AttackingState : State
    {
        private State _attackandretreat;
        public AggressorDataHolder DataHolder;

        protected override void Start()
        {
            base.Start();
            StateName = "Attacking";
            _attackandretreat = GetComponent<AttackAndRetreatState>();
            DataHolder = (_stateMachine as AggressorFsm).dataHolder;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering " + this.GetType().FullName);
        }

        /// <summary>
        /// Example Execute function that transitions from Moving to Running.
        /// </summary>
        public override void Execute()
        {




            //turn to look at enemy 
            Quaternion targetrotation = Quaternion.LookRotation(DataHolder.EnemyPosition);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, 0.2f);
            //stand still and fire at the enmey 

            Debug.DrawLine(transform.position, DataHolder.EnemyPosition, Color.red);
                                          
            //if they are walking away keep them within attack distance

            // if they get close change to next state

            

        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting " + this.GetType().FullName);
        }

        private bool HandleMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    DataHolder.target = new Vector3(hit.point.x, .5f, hit.point.z);
                    return true;
                }
            }

            return false;
        }






    }
}


