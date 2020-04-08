using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using System;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AttackAndRetreatState : State
    {
        private State _defendingState;
        private State _attackingState;
        public AggressorDataHolder DataHolder;
        private bool fireing = true;
        private bool movingToDefend = false;
        //dont keep this here this should probably be moved to agent
        private float satisfaction_radius = 15;

        protected override void Start()
        {
            StateName = "AttackAndRetreat";
            _defendingState = GetComponent<DefendingState>();
            _attackingState = GetComponent<AttackingState>();
            base.Start();
            
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

                Debug.DrawLine(transform.position, DataHolder.EnemyPosition.Value, Color.red, DataHolder.weapon.rateOfFire * 1 / 2, true);
            }
        }

        /// <summary>
        /// Example Execute function that transitions from Moving to Running.
        /// </summary>
        public override void Execute()
        {

            if (Vector3.Distance(transform.position, DataHolder.EnemyPosition.Value) >= DataHolder.retreatDistance && DataHolder.defend_target == null)
            {
                //DataHolder.defend_target = null;
                StateMachine.ResetToDefaultState();
            }

           
            //if the target is null attempt to set a new target 
            if (DataHolder.defend_target == null && !movingToDefend)
            {
                Vector3 nearestdefencepoint;
                movingToDefend = CheckIfNearDefensePoint(out nearestdefencepoint);
                if (movingToDefend)
                {
                    DataHolder.defend_target = nearestdefencepoint;
                }

            }
            else
            {
                movingToDefend = true;
            }

            if (movingToDefend)
            {

                if (Vector3.Distance(transform.position, DataHolder.defend_target.Value) >= satisfaction_radius)
                {
                    transform.position = Vector3.MoveTowards(transform.position, DataHolder.defend_target.Value, 6f * Time.deltaTime);
                }
                else
                {
                    // change state to defending
                    movingToDefend = false;
                    StateMachine.ChangeState(_defendingState);
                }


            }
            else
            {
                Vector3 posdiff = DataHolder.EnemyPosition.Value - transform.position;

                Vector3 absposdiff = new Vector3(Math.Abs(posdiff.x), Math.Abs(posdiff.y), Math.Abs(posdiff.z));

                float theta = 1e-5f;

                Vector3 am = new Vector3(posdiff.x / (absposdiff.x + theta), 0, posdiff.z / (absposdiff.z + theta));

                DataHolder.move_target = transform.position + -am * 2.0f;

                if (transform.position != DataHolder.move_target)
                {
                    transform.position = Vector3.MoveTowards(transform.position, DataHolder.move_target.Value, 6f * Time.deltaTime);
                }
                else
                {
                    // Reach destination
                    StateMachine.ResetToDefaultState();
                }
            }

            Quaternion targetrotation = Quaternion.LookRotation(DataHolder.EnemyPosition.Value - transform.position);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, 0.8f);







        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting " + this.GetType().FullName);
            fireing = false;
            StopCoroutine(shoot());
        }

        private bool CheckIfNearDefensePoint(out Vector3 closestDefencePoint)
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, 30, DataHolder.defenceLayer.Value);
            
            if (colliders.Length > 0)
            {
                closestDefencePoint = colliders[0].transform.position;
                foreach (Collider c in colliders)
                {
                    if (Vector3.Distance(transform.position,closestDefencePoint) > Vector3.Distance(transform.position, c.transform.position))
                    {
                        closestDefencePoint = c.transform.position;
                    }


                }

                return true;
            }

            closestDefencePoint = new Vector3();

            return false;
        }






    }
}


