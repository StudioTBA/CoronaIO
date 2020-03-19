using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.StudioTBD.CoronaIO.FMS.Example;
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

        //if applicable
        public float AOE_radius;

        public Weapon(weaponType weapontype, float range, float damage, float radius)
        {
            this.type = weapontype;
            this.Range = range;
            this.attackdamage = damage;
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
            stateMachine.Start();
            
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


