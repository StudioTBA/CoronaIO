using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie.States
{
    public class Zombie_Wander : State
    {
        private ZombieDataHolder _dataHolder;
        private State _idle;
        private State _seekClosestHuman;

        public float delay;
        private float timer = 0;

        protected override string SetStateName()
        {
            return "Wander";
        }

        protected override void OnStart()
        {
            // StateName = "Wander";
            _dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
            _idle = GetComponent<Idle_Zombie>();
            _seekClosestHuman = GetComponent<SeekClosestHuman>();
        }

        public override void OnStateEnter()
        {
            InvokeRepeating("WanderToNewPosition",0,2);
            
            base.OnStateEnter();
        }

        public override void Execute()
        {
            timer += Time.deltaTime;

            //Must get here from idle state only if not currently controlled by player
            if (_dataHolder.FlockManager.stop)
            {
                this.ChangeState(_idle);
                CancelInvoke();
                _dataHolder.NavMeshAgent.ResetPath();
            }
            else if (timer > delay)
            {

                timer = 0;
                if (!_dataHolder.Target)
                    this.ChangeState(_seekClosestHuman);
            }

        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is ZombieEvent zombieEvent)) return;

            //TODO: Somehow get StateMachine to change state through a player controlled command
        }

        private void WanderToNewPosition()
        {
            Vector3 newPos = RandomPoint(transform.position, 1000f, -1);
            this._dataHolder.NavMeshAgent.SetDestination(newPos);
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