using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.FMS.Example;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AggressorFsm : StateMachine
    {
        public AggressorDataHolder DataHolder;

        public AggressorFsm(AggressorDataHolder dataHolder)
        {
            this.DataHolder = dataHolder;
        }
    }
}

