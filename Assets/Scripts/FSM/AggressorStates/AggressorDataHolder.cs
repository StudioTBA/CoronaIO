using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.FMS.Example;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AggressorDataHolder : DataHolder
    {
        public enum weaponType { Aoe, longrange, mediumrange, shortrange}

        public weaponType? weapon;

        public Vector3? EnemyPosition;

        public LayerMask? enemyLayer;

    }
}


