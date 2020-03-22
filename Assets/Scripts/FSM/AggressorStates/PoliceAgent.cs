using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.StudioTBD.CoronaIO.Agent.Human;
using System;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
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

    public class PoliceAgent : MonoBehaviour
    {
   
        public Camera _camera;
        public AggressorFsm stateMachine;
        public State _defaultState;
        public Text stateText;
        [SerializeField] private LayerMask enemylayer;
        [SerializeField] private float retreatDistance;
        [SerializeField] private Weapon weapon;
        private AggressorDataHolder _dataHolder = new AggressorDataHolder();


        private void Awake()
        {

            stateMachine = new AggressorFsm(_dataHolder);
            stateMachine.Setup(gameObject, _defaultState);

        }

        public void Start()
        {
            stateMachine.dataHolder.enemyLayer = enemylayer;
            stateMachine.dataHolder.weapon = weapon;
            stateMachine.dataHolder.retreatDistance = retreatDistance;
            StartCoroutine(checkforEnemies());
            stateMachine.Start();
            
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

                Collider[] colliders = Physics.OverlapSphere(transform.position, 20, stateMachine.dataHolder.enemyLayer.Value);
                if (colliders.Length > 0)
                {
                    Vector3 smallestpos = colliders[0].transform.position;

                    foreach (Collider c in colliders)
                    {
                        Vector3 temppos = c.transform.position;

                        smallestpos = Vector3.Distance(transform.position, temppos) < Vector3.Distance(transform.position, smallestpos) ? c.transform.position : smallestpos;

                        stateMachine.dataHolder.EnemyPosition = smallestpos;
                    }

                }



            }



        }
        // Update is called once per frame
        void Update()
        {
            stateMachine.Execute();
   

            if (stateMachine.CurrentState != null)
            {
                stateText.text = "State: " + stateMachine.CurrentState.StateName;
            }
            else
                stateText.text = "State: Null";
        }
    }
}


