using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie.States
{
    public class Zombie_Arrive : State
    {
        ZombieDataHolder dataHolder;
        State seekClosestHuman;

        protected override string SetStateName()
        {
            return "Arrive";
        }

        protected override void OnStart()
        {
            seekClosestHuman = GetComponent<SeekClosestHuman>();
        }

        public override void OnStateEnter()
        {
            dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
            dataHolder.Target = null;
        }


        public override void Execute()
        {
            if (this.CheckAndTransitionToArrive(dataHolder))
                return;

            if (!dataHolder.NavMeshAgent.pathPending)
            {
                if (dataHolder.NavMeshAgent.remainingDistance <= dataHolder.NavMeshAgent.stoppingDistance)
                {
                    if (!dataHolder.NavMeshAgent.hasPath || dataHolder.NavMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        Destroy(dataHolder.myArriveParticleFX);
                        this.ChangeState(seekClosestHuman);
                    }
                }
            }


        }


    }
}
