using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.Agent.Example
{
    public class HumanStateMachine : StateMachine
    {
        public DataHolder DataHolder;

        public HumanStateMachine(DataHolder dataHolder)
        {
            DataHolder = dataHolder;
        }
    }
}