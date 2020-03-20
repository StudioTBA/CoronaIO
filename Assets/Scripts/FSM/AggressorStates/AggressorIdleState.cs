using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using Com.StudioTBD.CoronaIO.Agent.Human;


namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AggressorIdleState : State
    {

        private State _walkingState;
        private State _attackingState;
        private AggressorDataHolder dataHolder;

        protected override void Start()
        {
            base.Start();
            StateName = "Idle";
            _walkingState = GetComponent<AggressorWalkingState>();
            _attackingState = GetComponent<AttackingState>();
            dataHolder = (StateMachine as AggressorFsm).dataHolder;
            //StartCoroutine(checkforEnemies());
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering " + this.GetType().FullName);
        }


        


        /// <summary>
        /// 
        /// checks if there is an enemy nearby
        /// if enemy is within range will change state to attacking 
        /// else if directional input is given will walk to a destination
        /// </summary>
        /// 
        public override void Execute()
        {
            

            if(dataHolder.EnemyPosition != null)
            {
                if (Vector3.Distance(transform.position, dataHolder.EnemyPosition) <= dataHolder.weapon.Range)
                {

                    this.ChangeState(_attackingState);

                }

            }



            if (HandleMouseClick())
            {
                this.ChangeState(_walkingState);
            }


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
                    dataHolder.target = new Vector3(hit.point.x, .5f, hit.point.z);
                    return true;
                }
            }

            return false;
        }
    }





}



