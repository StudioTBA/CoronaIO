using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using Agents.Aggressor;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class DefendingState : State
    {
        private State _attack;
        public AggressorDataHolder DataHolder;
        private bool fireing = true;
        
        protected override string SetStateName()
        {
            return "Defending";
        }

        protected override void OnStart()
        {
            _attack = GetComponent<AttackingState>();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
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

                var position = transform.position + (transform.forward * 50f);
                var bulletGameObject =
                    Instantiate(DataHolder.weapon.BulletPrefab, position, transform.rotation);
                var bullet = bulletGameObject.GetComponent<Bullet>();
                bullet.transform.localScale = transform.localScale;
                bullet.Shoot(transform.forward);


                //Debug.DrawLine(transform.position, DataHolder.EnemyPosition.Value, Color.red,
                //    DataHolder.weapon.rateOfFire * 1 / 2, true);
            }
        }

        public override void Execute()
        {
            //turn to look at enemy 
            Quaternion targetrotation = new Quaternion();
            if (DataHolder.EnemyPosition.HasValue)
                targetrotation = Quaternion.LookRotation(DataHolder.EnemyPosition.Value - transform.position);
            else
                targetrotation = Quaternion.LookRotation(transform.position - DataHolder.defend_target.Value);
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