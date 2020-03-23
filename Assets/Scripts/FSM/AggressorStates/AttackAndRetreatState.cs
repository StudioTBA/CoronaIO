﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent.Human;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using System;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AttackAndRetreatState : State
    {
        private State _defendingState;
        //private State _attackingState;
        public AggressorDataHolder DataHolder;
        private bool fireing = true;

        protected override void Start()
        {
            base.Start();
            StateName = "AttackAndRetreat";
            _defendingState = GetComponent<DefendingState>();
            //_attackingState = GetComponent<AttackingState>();
            
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering " + this.GetType().FullName);
            fireing = true;
            
            DataHolder = DataHolder == null? (_stateMachine as AggressorFsm).dataHolder: DataHolder;
            StartCoroutine(shoot());
        }

        IEnumerator shoot()
        {
            if (Time.timeSinceLevelLoad < 0.3f)
                yield return new WaitForSeconds(0.3f);
            //stand still and fire at the enmey 
            //probably want to trigger an effect here not just draw line
            while (fireing)
            {
                yield return new WaitForSeconds(DataHolder.weapon.rateOfFire);

                Debug.DrawLine(transform.position, DataHolder.EnemyPosition, Color.red, DataHolder.weapon.rateOfFire * 1 / 2, true);
            }
        }

        /// <summary>
        /// Example Execute function that transitions from Moving to Running.
        /// </summary>
        public override void Execute()
        {

            if (Vector3.Distance(transform.position, DataHolder.EnemyPosition) <= DataHolder.retreatDistance)
            {
                StateMachine.ResetToDefaultState();
            }

            
            //set new retreat target.

            Vector3 posdiff = DataHolder.EnemyPosition - transform.position;
            Vector3 absposdiff = new Vector3(Math.Abs(posdiff.x), Math.Abs(posdiff.y), Math.Abs(posdiff.z));
            float theta = 1e-5f;
            Vector3 am = new Vector3(posdiff.x / (absposdiff.x + theta), 0, posdiff.z / (absposdiff.z + theta));

            DataHolder.target = transform.position + -am * 2.0f;

            Quaternion targetrotation = Quaternion.LookRotation(DataHolder.EnemyPosition - transform.position);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, 0.8f);

            

            if (transform.position != DataHolder.target.Value)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, DataHolder.target.Value, 6f * Time.deltaTime);
            }
            else
            {
                // Reach destination
                StateMachine.ResetToDefaultState();
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    this.ChangeState(_defendingState);
            //}
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting " + this.GetType().FullName);
            fireing = false;
            StopCoroutine(shoot());
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

