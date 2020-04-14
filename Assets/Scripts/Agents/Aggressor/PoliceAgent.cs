using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Aggressors;
using System;
using Com.StudioTBD.CoronaIO.Agent.Human;
using Com.StudioTBD.CoronaIO.Agent.Zombie;
using JetBrains.Annotations;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.Agent.Aggressors
{
    public enum weaponType
    {
        Aoe,
        longrange,
        mediumrange,
        shortrange
    }

    [Serializable]
    public struct Weapon
    {
        public weaponType type;

        public float Range;

        public float attackdamage;

        public float rateOfFire;

        //if applicable
        public float AOE_radius;

        public Weapon(weaponType weapontype, float range, float damage, float rateoffire, float radius)
        {
            this.type = weapontype;
            this.Range = range;
            this.attackdamage = damage;
            this.rateOfFire = rateoffire;
            this.AOE_radius = weapontype == weaponType.Aoe ? radius : 0;
        }
    }

    public class PoliceAgent : Agent
    {
        [SerializeField] private LayerMask enemylayer;
        [SerializeField] private LayerMask defencelayer;
        [SerializeField] private float retreatDistance;
        [SerializeField] private Weapon weapon;
        [SerializeField] public float SightDistance = 1000f;
        [SerializeField] public float SightHeight = 1f;
        [CanBeNull] private GameManager _gameManager;
        private AggressorDataHolder _dataHolder = new AggressorDataHolder();

        public bool IsDebug = false;

        protected override void Awake()
        {
            base.Awake();
            stateMachine = new AggressorFsm(_dataHolder);
            stateMachine.Setup(gameObject, defaultState);
            _dataHolder.enemyLayer = enemylayer;
            _dataHolder.defenceLayer = defencelayer;
            _dataHolder.weapon = weapon;
            _dataHolder.retreatDistance = retreatDistance;
            _dataHolder.agent_sight = SightDistance;
            _dataHolder.NavMeshAgent = GetComponent<NavMeshAgent>();
            _gameManager = FindObjectOfType<GameManager>();
            StartCoroutine(checkforEnemies());
        }

        IEnumerator checkforEnemies()
        {
            //wait for the game to load before starting the coroutine
            if (Time.timeSinceLevelLoad < 0.3f)
                yield return new WaitForSeconds(0.3f);

            while (true)
            {
                //wait for a second before continuing to update path 
                yield return new WaitForSeconds(1.0f);

                // Find all zombies in an area
                Collider[] colliders =
                    Physics.OverlapSphere(transform.position, SightDistance, _dataHolder.enemyLayer.Value);

                if (colliders.Length == 0)
                {
                    _dataHolder.EnemyPosition = null;
                    if (IsDebug) Debug.Log($"Not collided");
                    continue;
                }
                else
                {
                    if (IsDebug) Debug.Log($"Found {colliders.Length} colliders");
                    GetClosestZombieInSight(colliders, SightDistance);
                }
            }
        }

        public void GetClosestZombieInSight(Collider[] zombies, float sightRange)
        {
            RaycastHit hit;
            List<GameObject> inSight = new List<GameObject>();

            var thisRawPos = this.transform.position;
            var thisPosition = new Vector3(thisRawPos.x, SightHeight, thisRawPos.z);

            GameObject closestZombie = null;
            float smallestDistance = float.MaxValue;
            Vector3 targetDir = Vector3.forward;
            // Get all in sight
            foreach (var zombie in zombies)
            {
                var targetRawPos = zombie.transform.position;
                var targetPosition = new Vector3(targetRawPos.x, SightHeight, targetRawPos.z);
                var direction = targetPosition - thisPosition;

                if (!Physics.Raycast(thisPosition, direction, out hit, sightRange)) continue;
                if (hit.collider.GetComponent<Flocker>() == null) continue;
                inSight.Add(zombie.gameObject);
                var distance = Vector3.Distance(thisRawPos, targetRawPos);

                if (smallestDistance > distance)
                {
                    targetDir = direction;
                    smallestDistance = distance;
                    closestZombie = zombie.gameObject;
                }
            }

            if (closestZombie == null)
            {
                this._dataHolder.EnemyPosition = null;
                return;
            }

            _dataHolder.EnemyPosition = closestZombie.transform.position;
            StartCoroutine(AlertClosestHumansInRange(sightRange));
        }


        public IEnumerator AlertClosestHumansInRange(float range)
        {
            // Collider[] humans =
            //     Physics.OverlapSphere(transform.position, SightDistance, LayerMask.GetMask(GameManager.Tags.HumanTag));


            var humans = Physics.OverlapSphere(transform.position, range,
                LayerMask.GetMask(GameManager.Tags.HumanTag));


            var civilians = new List<HumanAgent>();

            // if (_gameManager == null) yield return null;
            foreach (var human in humans)
            {
                var distance = Vector3.Distance(human.transform.position, gameObject.transform.position);
                if (distance < range)
                {
                    //Debug.Log("In range");
                    //Debug.Log($"In range {human.name}");
                    
                    HumanAgent humanAgent = human.transform.parent.GetComponent<HumanAgent>();

                    if (humanAgent != null)
                    {
                        civilians.Add(humanAgent);
                    }
                }
                else
                {
                    //Debug.Log("Not in range");
                }
            }

            foreach (var civilian in civilians)
            {
                //Debug.Log($"Alerting civilian in range {civilian.name}", this);
                civilian.Consume(new HumanEvent(this.gameObject, HumanEvent.HumanEventType.PoliceAlert));
            }

            yield return null;
        }

        private void OnDrawGizmos()
        {
            if (!IsDebug) return;
            var Radius = SightDistance;
            var T = transform;
            Gizmos.color = Color.white;
            var theta = 0f;
            var x = Radius * Mathf.Cos(theta);
            var y = Radius * Mathf.Sin(theta);
            var pos = T.position + new Vector3(x, 0, y);
            var newPos = pos;
            var lastPos = pos;
            for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
            {
                x = Radius * Mathf.Cos(theta);
                y = Radius * Mathf.Sin(theta);
                newPos = T.position + new Vector3(x, 0, y);
                Gizmos.DrawLine(pos, newPos);
                pos = newPos;
            }

            Gizmos.DrawLine(pos, lastPos);

            //
            // Gizmos.color = new Color(.5f, .5f, .5f, .2f);
            // Gizmos.DrawSphere(transform.position, SightDistance);

            // if (!IsDebug) return;
            // Gizmos.color = new Color(.5f, .5f, .2f, .2f);
            // Gizmos.DrawSphere(transform.position, weapon.Range);
            Radius = weapon.Range;
            T = transform;
            theta = 0f;
            x = Radius * Mathf.Cos(theta);
            y = Radius * Mathf.Sin(theta);
            pos = T.position + new Vector3(x, 0, y);
            newPos = pos;
            lastPos = pos;
            for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
            {
                x = Radius * Mathf.Cos(theta);
                y = Radius * Mathf.Sin(theta);
                newPos = T.position + new Vector3(x, 0, y);
                Gizmos.DrawLine(pos, newPos);
                pos = newPos;
            }

            Gizmos.DrawLine(pos, lastPos);

            if (!_dataHolder.EnemyPosition.HasValue) return;
            Gizmos.color = new Color(1f, 1f, 1f, 1f);
            Gizmos.DrawLine(this.transform.position, _dataHolder.EnemyPosition.Value);
        }
    }
}