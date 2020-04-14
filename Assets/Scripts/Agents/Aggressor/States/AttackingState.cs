using System.Collections;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using System;
using Agents.Aggressor;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AttackingState : State
    {
        private State _attackandretreat;
        private State _walkingState;
        public AggressorDataHolder DataHolder;

        private bool fireing = true;


        protected override string SetStateName()
        {
            return "Attacking";
        }

        protected override void OnStart()
        {
            _attackandretreat = GetComponent<AttackAndRetreatState>();
            _walkingState = GetComponent<AggressorWalkingState>();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            fireing = true;
            DataHolder = DataHolder == null ? (_stateMachine as AggressorFsm).dataHolder : DataHolder;
            StartCoroutine(shoot());
        }

        /// <summary>
        /// Example Execute function that transitions from Moving to Running.
        /// </summary>
        public override void Execute()
        {
            //stop animation if velocity is zero
            if (DataHolder.NavMeshAgent.remainingDistance <= DataHolder.NavMeshAgent.stoppingDistance)
            {
                if (!DataHolder.NavMeshAgent.hasPath || DataHolder.NavMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    DataHolder.Animator.SetBool("Walking", false);
                }
            }
            else
                DataHolder.Animator.SetBool("Walking", true);


            if (DataHolder.weapon.type == weaponType.shortrange)
                this.DataHolder.Animator.SetBool("ShootingPistol", true);
            else
                this.DataHolder.Animator.SetBool("ShootingRifle", true);


            if (!DataHolder.EnemyPosition.HasValue)
            {
                this.ResetToDefaultState();
                return;
            }

            //turn to look at enemy 
            Quaternion targetrotation = Quaternion.LookRotation(DataHolder.EnemyPosition.Value - transform.position);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, 0.8f);

            //if they are walking away keep them within attack distance
            if (Vector3.Distance(transform.position, DataHolder.EnemyPosition.Value) >= DataHolder.weapon.Range - 1)
            {
                this.DataHolder.Animator.SetBool("Walking", true);
                float distdiff = (Vector3.Distance(transform.position, DataHolder.EnemyPosition.Value) -
                                  DataHolder.weapon.Range);
                DataHolder.move_target = transform.position +
                                         (DataHolder.EnemyPosition.Value - transform.position).normalized *
                                         Math.Abs(distdiff);
                StateMachine.ChangeState(_walkingState);
            }


            // if they get close change to next state
            if (Vector3.Distance(transform.position, DataHolder.EnemyPosition.Value) <= DataHolder.retreatDistance)
            {
                StateMachine.ChangeState(_attackandretreat);
            }

            Vector3 nearestdefencepoint;
            if (CheckIfNearDefensePoint(out nearestdefencepoint))
            {
                DataHolder.defend_target = nearestdefencepoint;

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
                
                var position = transform.position + (transform.forward * 50f);
                var bulletGameObject =
                    Instantiate(DataHolder.weapon.BulletPrefab, position, transform.rotation);
                var bullet = bulletGameObject.GetComponent<Bullet>();
                bullet.transform.localScale = new Vector3(10f, 10f, 10f);
                bullet.Shoot(transform.forward);
                // bullet.Shoot(DataHolder.EnemyPosition.Value - transform.position);

                // Debug.DrawLine(transform.position, DataHolder.EnemyPosition.Value, Color.red,
                //     DataHolder.weapon.rateOfFire * 1 / 2, true);
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

        private bool CheckIfNearDefensePoint(out Vector3 closestDefencePoint)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 30, DataHolder.defenceLayer.Value);

            if (colliders.Length > 0)
            {
                closestDefencePoint = colliders[0].transform.position;
                foreach (Collider c in colliders)
                {
                    if (Vector3.Distance(transform.position, closestDefencePoint) >
                        Vector3.Distance(transform.position, c.transform.position))
                    {
                        closestDefencePoint = c.transform.position;
                    }
                }

                return true;
            }

            closestDefencePoint = new Vector3();

            return false;
        }


        public override void Consume(Event.Event @event)
        {
            if (!(@event is HumanEvent humanEvent)) return;

            switch (humanEvent.EventType)
            {
                case HumanEvent.HumanEventType.SpottedZombie:
                    break;
                case HumanEvent.HumanEventType.PoliceAlert:
                    DataHolder.defend_target = @event.Producer.transform.position;
                    this.ChangeState(_attackandretreat);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}