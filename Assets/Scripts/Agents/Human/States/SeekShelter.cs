using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
//using UnityEditor.Presets;
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

        protected override string SetStateName()
        {
            return "Seek Shelter";
        }

        protected override void Awake()
        {
            base.Awake();
            // StateName = "Seek Shelter";
        }

        protected override void OnStart()
        {
            _dataHolder = (StateMachine as HumanStateMachine)?.DataHolder;
            _fleeState = GetComponent<Flee>();
            _fleeToShelterState = GetComponent<FleeToShelter>();
            _gameManager = FindObjectOfType<GameManager>();
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
            //Debug.Log("Start Coroutine: Find shelter", this);
            NavMeshPath shortestPath = new NavMeshPath();

            var colliders = Physics.OverlapSphere(transform.position, _dataHolder.SeekShelterRange,
                LayerMask.GetMask("Shelter"));
            Debug.Log($"Found {colliders.Length} shelters");
            NavMeshAgent agent = _dataHolder.NavMeshAgent;
            // GameObject[] shelters = _gameManager.Shelters;
            float smallestDistance = float.MaxValue;
            int smallestDistanceIndex = -1;

            for (var index = 0; index < colliders.Length; index++)
            {
                var shelter = colliders[index].transform.GetChild(0).GetComponent<Shelter>();
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

            if (smallestDistanceIndex >= 0 && colliders.Length > 0)
            {
                Debug.Log("Closest Shelter: " + colliders[smallestDistanceIndex]);
                _dataHolder.Target =
                    colliders[smallestDistanceIndex].transform.GetChild(0).gameObject;
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