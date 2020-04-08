using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.Agent.Human.States
{
    public class SeekShelter : State
    {
        private DataHolder _dataHolder;
        private State _fleeState;
        private State _fleeToShelterState;
        private GameManager _gameManager;

        protected override void Awake()
        {
            base.Awake();
            StateName = "Seek Shelter";
        }

        protected override void Start()
        {
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
            _fleeState = GetComponent<Flee>();
            _fleeToShelterState = GetComponent<FleeToShelter>();
            _gameManager = FindObjectOfType<GameManager>();
            base.Start();
        }

        private Coroutine _searchShelterCoroutine = null;


        public override void Execute()
        {
            if (_searchShelterCoroutine != null) return;
            _searchShelterCoroutine = StartCoroutine(FindShelter());
        }

        /// <summary>
        /// Assume there is at least 1 shelter available
        /// </summary>
        /// <returns></returns>
        private IEnumerator FindShelter()
        {
            Debug.Log("Start Coroutine: Find shelter", this);
            NavMeshPath shortestPath = new NavMeshPath();

            NavMeshAgent agent = _dataHolder.NavMeshAgent;
            GameObject[] shelters = _gameManager.Shelters;
            float smallestDistance = float.MaxValue;
            int smallestDistanceIndex = 0;

            for (var index = 0; index < shelters.Length; index++)
            {
                var shelter = shelters[index].GetComponent<Shelter>();
                var position = shelter.floorMeshRenderer.transform.position;
                agent.CalculatePath(position, shortestPath);

                float distance = 0;
                for (var i = 0; i < shortestPath.corners.Length - 1; i++)
                {
                    distance += Vector3.Distance(shortestPath.corners[i], shortestPath.corners[i + 1]);
                }

                if (distance <= smallestDistance)
                {
                    smallestDistance = distance;
                    smallestDistanceIndex = index;
                }

                agent.isStopped = true;
            }

            if (smallestDistanceIndex >= 0)
            {
                Debug.Log("Closest Shelter: " + shelters[smallestDistanceIndex]);
                _dataHolder.Target =
                    shelters[smallestDistanceIndex];
                this.ChangeState(_fleeToShelterState);
            }
            else
            {
                this.ChangeState(_fleeState);
            }

            yield return null;
        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            StopAllCoroutines();
        }
    }
}