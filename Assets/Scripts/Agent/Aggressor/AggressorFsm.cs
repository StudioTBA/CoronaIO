using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;

namespace Com.StudioTBD.CoronaIO.FMS.Aggressors
{
    public class AggressorFsm : StateMachine
    {
        public AggressorDataHolder dataHolder;

        public AggressorFsm(AggressorDataHolder dataHolder)
        {
            this.dataHolder = dataHolder;
        }
    }
}

