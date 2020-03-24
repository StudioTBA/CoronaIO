using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Aggressors;
using System;

namespace Com.StudioTBD.CoronaIO.Agent.Aggressors
{

    public enum weaponType { Aoe, longrange, mediumrange, shortrange }

    [Serializable]
    public struct Weapon
    {

        public weaponType type;

        public float Range;

        public float attackdamage;

        public float rateOfFire;

        //if applicable
        public float AOE_radius;

        public Weapon(weaponType weapontype, float range, float damage, float rateoffire,float radius)
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
   
        //public Camera _camera;
        //public AggressorFsm stateMachine;
        //public State _defaultState;
        //public Text stateText;
        [SerializeField] private LayerMask enemylayer;
        [SerializeField] private LayerMask defencelayer;
        [SerializeField] private float retreatDistance;
        [SerializeField] private Weapon weapon;
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
            StartCoroutine(checkforEnemies());

        }

        //public void Start()
        //{
            
        //    stateMachine.Start();
            
        //}

        IEnumerator checkforEnemies()
        {
            //wait for the game to load before starting the coroutine
            if (Time.timeSinceLevelLoad < 0.3f)
                yield return new WaitForSeconds(0.3f);


            while (true)
            {
                //wait for a second before continuing to update path 
                yield return new WaitForSeconds(1.0f);

                Collider[] colliders = Physics.OverlapSphere(transform.position, 20, _dataHolder.enemyLayer.Value);
                if (colliders.Length > 0)
                {
                    Vector3 smallestpos = colliders[0].transform.position;

                    foreach (Collider c in colliders)
                    {
                        Vector3 temppos = c.transform.position;

                        smallestpos = Vector3.Distance(transform.position, temppos) < Vector3.Distance(transform.position, smallestpos) ? c.transform.position : smallestpos;

                        _dataHolder.EnemyPosition = smallestpos;
                    }

                }



            }

        }
        
        







    }
}


