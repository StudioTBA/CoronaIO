﻿using System.Collections;
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
        State wander;

        protected override string SetStateName()
        {
            return "Arrive";
        }

        protected override void OnStart()
        {
            wander = GetComponent<Zombie_Wander>();
        }

        public override void OnStateEnter()
        {
            dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
        }


        public override void Execute()
        {
            this.CheckAndTransitionToArrive(this, dataHolder);

            if (!dataHolder.NavMeshAgent.pathPending)
            {
                if (dataHolder.NavMeshAgent.remainingDistance <= dataHolder.NavMeshAgent.stoppingDistance)
                {
                    if (!dataHolder.NavMeshAgent.hasPath || dataHolder.NavMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        this.ChangeState(wander);
                    }
                }
            }
        }


    }
}
