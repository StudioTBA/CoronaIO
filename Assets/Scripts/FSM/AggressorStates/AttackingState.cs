using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent.Human;
using Com.StudioTBD.CoronaIO.FMS.Extensions;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AttackingState : State
    {
        private State _attackandretreat;
        public AggressorDataHolder DataHolder;

        private bool fireing = true;
        protected override void Start()
        {
            base.Start();
            StateName = "Attacking";
            _attackandretreat = GetComponent<AttackAndRetreatState>();
           
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering " + this.GetType().FullName);
            fireing = true;
            DataHolder = DataHolder == null ? (_stateMachine as AggressorFsm).dataHolder : DataHolder;
            StartCoroutine(shoot());

        }

        /// <summary>
        /// Example Execute function that transitions from Moving to Running.
        /// </summary>
        public override void Execute()
        {




            //turn to look at enemy 
            Quaternion targetrotation = Quaternion.LookRotation(DataHolder.EnemyPosition - transform.position);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, 0.8f);
                                          
            //if they are walking away keep them within attack distance

            // if they get close change to next state
            if (Vector3.Distance(transform.position, DataHolder.EnemyPosition) <= DataHolder.retreatDistance)
            {
                StateMachine.ChangeState(_attackandretreat);
            }
            

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
        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting " + this.GetType().FullName);
            fireing = false;
            StopCoroutine(shoot());
            //StopAllCoroutines();
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


