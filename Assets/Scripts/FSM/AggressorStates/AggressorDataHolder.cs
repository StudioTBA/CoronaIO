using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent.Human;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AggressorDataHolder : DataHolder
    {
        public Vector3? target;

        public Weapon weapon;

        public Vector3 EnemyPosition;

        public LayerMask? enemyLayer;

        public float retreatDistance = 3.0f;

    }
}


