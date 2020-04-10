using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie.States
{
    public class SeekClosestHuman : State
    {
        private ZombieDataHolder _dataHolder;
        private State _attack;
        private State _idle;
        private State _flee;
        public float range;
        [Tooltip("If the value obtained by dividing the number of humans around target to the horde size " +
            "is less than this value, the horde will attack. Otherwise it will flee")]
        public float minRatioToAttack;


        protected override string SetStateName()
        {
            return "SeekClosestHuman";
        }

        protected override void OnStart()
        {
            // StateName = "SeekClosestHuman";
            _dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
            _attack = GetComponent<Zombie_Attack>();
            _idle = GetComponent<Idle_Zombie>();
            _flee = GetComponent<Zombie_Flee>();
        }

        public override void Execute()
        {
            //Set target to closest human
            FindClosestHuman();

            //print(_dataHolder.Target);
            if (_dataHolder.Target)
            {
                if (_dataHolder.FlockManager.always_flee)
                    this.ChangeState(_flee);
                else if (_dataHolder.FlockManager.attack_if_able)
                    this.ChangeState(_attack);
                else if (OverwhelmingTheHumans())
                    this.ChangeState(_attack);
                else
                    this.ChangeState(_flee);
            }
            else
                this.ChangeState(_idle);
        }

        public override void Consume([NotNull] Event.Event @event)
        {
            if (!(@event is ZombieEvent humanEvent)) return;

            //TODO: Somehow get StateMachine to change state through a player controlled command
        }

        private void FindClosestHuman()
        {
            GameObject[] agents = GameObject.FindGameObjectsWithTag("Agent");

            GameObject temp = null;

            float dist = float.MaxValue;
            float toCompare;

            foreach(GameObject obj in agents)
            {
               toCompare = (obj.transform.position - transform.position).magnitude;
               if (toCompare<range && toCompare < dist)
               {
                    temp = obj;
                    dist = toCompare;
               }
            }

            _dataHolder.Target = temp;
        }

        private bool OverwhelmingTheHumans()
        {
            Collider[] colliders = Physics.OverlapSphere(_dataHolder.Target.transform.position, 200);

            int numOfHumans = 1;

            foreach(Collider coll in colliders)
            {
                if(coll.gameObject.tag == "Agent")
                {
                    numOfHumans++;
                }
            }

            int numOfZombies = _dataHolder.FlockManager.getZombieList().Count;

            return numOfHumans / (numOfZombies == 0 ? numOfHumans : numOfZombies)<minRatioToAttack;
        }
    }
}