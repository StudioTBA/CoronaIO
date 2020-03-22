using System;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    public class Moving : State
    {
        private DataHolder _dataHolder;
        private State _fleeState;

        protected override void Start()
        {
            base.Start();
            StateName = "Moving";
            _dataHolder = (StateMachine as HumanStateMachine).DataHolder;
            _fleeState = GetComponent<Flee>();
        }

        public override void Execute()
        {
            switch (_dataHolder.NavMeshAgent.pathStatus)
            {
                case NavMeshPathStatus.PathComplete:
                case NavMeshPathStatus.PathInvalid:
                    if (_dataHolder.NavMeshAgent.remainingDistance <= 1f)
                    {
                        StateMachine.ResetToDefaultState();
                    }

                    break;

                case NavMeshPathStatus.PathPartial:
                    break;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 target = new Vector3(hit.point.x, 50f, hit.point.z);
                    _dataHolder.NavMeshAgent.SetDestination(target);
                }
            }
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            if (_dataHolder == null)
            {
                Debug.Log("Moving - Enter: DataHolder null", this);
            }
            else
            {
                Debug.Log("Moving - Enter: DataHolder NOT null", this);
            }

            // if (_dataHolder.NavMeshAgent == null)
            // {
            //     Debug.Log("Moving - Enter: DataHolder null", this);
            // }

            // Debug.Log("Moving - Enter", this);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            if (_dataHolder == null)
            {
                Debug.Log("Moving - Exit: DataHolder null", this);
            }
            else
            {
                Debug.Log("Moving - Exit: DataHolder NOT null", this);
            }
        }

        public override void Consume(Event.Event @event)
        {
            Debug.Log("Consuming event: " + @event, this);
            if (!(@event is HumanEvent humanEvent)) return;

            switch (humanEvent.EventType)
            {
                case HumanEvent.HumanEventType.SpottedZombie:
                    this.ChangeState(_fleeState);
                    break;
                case HumanEvent.HumanEventType.PoliceAlert:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}