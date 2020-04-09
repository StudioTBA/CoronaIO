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
        public float range;

        protected override void OnStart()
        {
            StateName = "SeekClosestHuman";
            _dataHolder = (StateMachine as ZombieStateMachine)?.ZombieDataHolder;
            _attack = GetComponent<Zombie_Attack>();
            _idle = GetComponent<Idle_Zombie>();
        }

        public override void Execute()
        {
            //Set target to closest human
            FindClosestHuman();

            //print(_dataHolder.Target);
            if (_dataHolder.Target)
                this.ChangeState(_attack);
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
            print(_dataHolder.Target);
        }
    }
}