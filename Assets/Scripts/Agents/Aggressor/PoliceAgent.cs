using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Aggressors;
using System;
using Com.StudioTBD.CoronaIO.Agent.Human;
using Com.StudioTBD.CoronaIO.Agent.Zombie;

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
        private GameManager _gameManager;
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
                    continue;
                }
                else
                {
                    GetClosestZombieInSight(colliders, SightDistance);
                }


                // Vector3 smallestpos = colliders[0].transform.position;
                //
                // RaycastHit hit;
            }
        }

        // foreach (Collider c in colliders)
        // {
        //     var targetRawPos = c.transform.position;
        //     var ownRawPos = this.transform.position;
        //     var targetPosition = new Vector3(targetRawPos.x, 20f, targetRawPos.z);
        //     var ownPosition = new Vector3(ownRawPos.x, 20f, ownRawPos.z);
        //
        //     Vector3 targetDir = targetPosition - ownPosition;
        //     Debug.DrawRay(ownPosition, targetDir, Color.red);
        //
        //     if (Physics.Raycast(ownPosition, targetDir, out hit,
        //         SightDistance))
        //     {
        //         // Raycast check if in sight
        //         if (hit.collider.GetComponent<Flocker>() != null)
        //         {
        //             Vector3 temppos = c.transform.position;
        //
        //             smallestpos =
        //                 Vector3.Distance(ownPosition, temppos) <
        //                 Vector3.Distance(ownPosition, smallestpos)
        //                     ? targetPosition
        //                     : smallestpos;
        //
        //             _dataHolder.EnemyPosition = smallestpos;
        //             // Notify closest humans
        //             StartCoroutine(AlertClosestHumansInRange(SightDistance));
        //         }
        //     }
        // }

        public void GetClosestZombieInSight(Collider[] zombies, float sightRange)
        {
            RaycastHit hit;
            List<GameObject> inSight = new List<GameObject>();

            var thisRawPos = this.transform.position;
            var thisPosition = new Vector3(thisRawPos.x, 10f, thisRawPos.z);

            GameObject closestZombie = null;
            float smallestDistance = float.MaxValue;
            Vector3 targetDir = Vector3.forward;
            // Get all in sight
            foreach (var zombie in zombies)
            {
                var targetRawPos = zombie.transform.position;
                var targetPosition = new Vector3(targetRawPos.x, 10f, targetRawPos.z);
                var direction = targetPosition - thisPosition;

                if (Physics.Raycast(thisPosition, direction, out hit, sightRange))
                {
                    if (hit.collider.GetComponent<Flocker>() != null)
                    {
                        inSight.Add(zombie.gameObject);
                        var distance = Vector3.Distance(thisRawPos, targetRawPos);
                        if (smallestDistance > distance)
                        {
                            targetDir = direction;
                            smallestDistance = distance;
                            closestZombie = zombie.gameObject;
                        }
                    }
                }
            }

            if (closestZombie != null)
            {
                _dataHolder.EnemyPosition = closestZombie.transform.position;
                // Debug.DrawRay(thisPosition, targetDir, Color.red, 10f);
                StartCoroutine(AlertClosestHumansInRange(sightRange));
            }
        }


        public IEnumerator AlertClosestHumansInRange(float range)
        {
            var civilians = new List<HumanAgent>();

            var humans = _gameManager.Humans;
            foreach (var human in humans)
            {
                var distance = Vector3.Distance(human.transform.position, gameObject.transform.position);
                if (distance < range)
                {
                    HumanAgent humanAgent = human.GetComponent<HumanAgent>();
                    if (humanAgent != null)
                    {
                        civilians.Add(humanAgent);
                    }
                }
            }

            foreach (var civilian in civilians)
            {
                Debug.Log($"Alerting civilian in range {civilian.name}", this);
                civilian.Consume(new HumanEvent(this.gameObject, HumanEvent.HumanEventType.PoliceAlert));
            }

            yield return null;
        }

        private void OnDrawGizmos()
        {
            if (!IsDebug) return;
            Gizmos.color = new Color(.5f, .5f, .5f, .2f);
            Gizmos.DrawSphere(transform.position, SightDistance);
            
            if (!_dataHolder.EnemyPosition.HasValue) return;
            Gizmos.color = new Color(1f, 1f, 1f, 1f);
            Gizmos.DrawLine(this.transform.position, _dataHolder.EnemyPosition.Value);
        }
    }
}