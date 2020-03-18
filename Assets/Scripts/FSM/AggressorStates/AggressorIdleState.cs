using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using Com.StudioTBD.CoronaIO.FMS.Example;


namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AggressorIdleState : State
    {

        private State _walkingState;
        private State _attackingState;
        private AggressorDataHolder DataHolder;

        protected override void Start()
        {
            base.Start();
            StateName = "Idle";
            _walkingState = GetComponent<WalkingState>();
            _attackingState = GetComponent<AttackingState>();
            DataHolder = (AggressorDataHolder)(StateMachine as AgentFsm).DataHolder;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering " + this.GetType().FullName);
        }

        /// <summary>
        /// Example Execute function that transitions from Idle to Moving.
        /// </summary>
        public override void Execute()
        {
            if (HandleMouseClick())
            {
                this.ChangeState(_walkingState);
            }

            if(DataHolder.EnemyPosition == null)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 5, DataHolder.enemyLayer.Value);

                Vector3 smallestpos = new Vector3();

                foreach(Collider c in colliders)
                {
                    Vector3 temppos = c.transform.position;

                    smallestpos = Vector3.Distance(transform.position,temppos) < Vector3.Distance(transform.position, smallestpos) ? c.transform.position : smallestpos;

                    DataHolder.EnemyPosition = smallestpos;
                }



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
                    DataHolder.target = new Vector3(hit.point.x, .5f, hit.point.z);
                    return true;
                }
            }

            return false;
        }
    }





}



