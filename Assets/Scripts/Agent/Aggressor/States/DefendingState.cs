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
        private bool fireing = true;

        protected override void Start()
        {
            StateName = "Defending";
            _attack = GetComponent<AttackingState>();
            base.Start();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering " + this.GetType().FullName);

            DataHolder = DataHolder == null ? (_stateMachine as AggressorFsm).dataHolder : DataHolder;
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

        public override void Execute()
        {
            //turn to look at enemy 
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

    }




}


