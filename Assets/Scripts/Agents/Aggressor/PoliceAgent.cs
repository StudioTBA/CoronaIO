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
        private GameManager _gameManager;
        private AggressorDataHolder _dataHolder = new AggressorDataHolder();

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

                Collider[] colliders = Physics.OverlapSphere(transform.position, 1000, _dataHolder.enemyLayer.Value);
                if (colliders.Length > 0)
                {
                    // Gizmos.DrawSphere(this.gameObject.transform.position, 2000);

                    Vector3 smallestpos = colliders[0].transform.position;


                    RaycastHit hit;

                    // All of this are zombies
                    foreach (Collider c in colliders)
                    {
                        // var targetPosition = c.transform.position;
                        // Vector3 targetDir = targetPosition - transform.position;
                        // Debug.DrawRay(transform.position, targetPosition, Color.red);
                        // if (Physics.Raycast(transform.position, targetDir, out hit,
                        //     2000))
                        // {
                        //     Debug.Log($"Collided with {hit.collider.name}");
                        //     // 1. Raycast check if in sight
                        //     if (hit.collider.GetComponent<ZombieAgent>() != null)
                        //     {
                        //        
                        //     }
                        // }
                        // else
                        // {
                        //     continue;
                        // }


                        // Found enemy
                        Vector3 temppos = c.transform.position;

                        smallestpos =
                            Vector3.Distance(transform.position, temppos) <
                            Vector3.Distance(transform.position, smallestpos)
                                ? c.transform.position
                                : smallestpos;

                        _dataHolder.EnemyPosition = smallestpos;
                        // Notify closest humans
                        StartCoroutine(AlertClosestHumansInRange(1000f));
                    }
                }
            }
        }

        public void GetClosestZombieInSight(int range)
        {
            Collider[] possibleTargets = Physics.OverlapSphere(transform.position, 2000, _dataHolder.enemyLayer.Value);


            foreach (var zombie in possibleTargets)
            {
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
            Gizmos.color = new Color(.5f, .5f, .5f, .2f);
            Gizmos.DrawSphere(transform.position, 1000);
        }
    }
}