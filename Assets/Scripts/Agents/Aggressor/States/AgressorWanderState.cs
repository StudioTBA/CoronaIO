using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AgressorWanderState : State
    {
        private State _runningState;
        private State _attackingState;
        public AggressorDataHolder DataHolder;
        private Vector3 currentTarget;


        protected override string SetStateName()
        {
            return "AgressorWanderState";
        }

        protected override void OnStart()
        {
            _attackingState = GetComponent<AttackingState>();
            DataHolder = (StateMachine as AggressorFsm).dataHolder;
        }

        public override void OnStateEnter()
        {
            InvokeRepeating("WanderToNewPosition", 0f, 2f);
            base.OnStateEnter();
        }

        public override void OnStateExit()
        {
            CancelInvoke();
            base.OnStateExit();
        }

        public override void Execute()
        {

            //stop animation if velocity is zero
            if (DataHolder.NavMeshAgent.velocity == Vector3.zero)
                this.DataHolder.Animator.SetBool("Walking", false);
            else
                this.DataHolder.Animator.SetBool("Walking", true);

            if (DataHolder.EnemyPosition != null)
            {
                if (Vector3.Distance(transform.position, DataHolder.EnemyPosition.Value) <= DataHolder.weapon.Range)
                {
                    
                    this.ChangeState(_attackingState);
                }
            }
        }

        private void WanderToNewPosition()
        {
            Vector3 newPos = RandomPoint(transform.position, 200f, -1);
            this.DataHolder.NavMeshAgent.SetDestination(newPos);
        }

        private Vector3 RandomPoint(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;

            randDirection += origin;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

            return navHit.position;
        }
    }
}