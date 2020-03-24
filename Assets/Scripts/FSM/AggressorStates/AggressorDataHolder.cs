using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent.Human;

namespace Com.StudioTBD.CoronaIO.Agent.Aggressors
{
    public class AggressorDataHolder : DataHolder
    {
        public Vector3? defend_target;

        public Vector3 move_target;

        public Weapon weapon;

        public Vector3 EnemyPosition;

        public LayerMask? enemyLayer;

        public LayerMask? defenceLayer;

        public float retreatDistance = 3.0f;

    }
}


