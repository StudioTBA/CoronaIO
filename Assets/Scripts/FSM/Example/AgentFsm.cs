using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.FMS.Example
{
    /// <summary>
    /// Example State Machine
    /// </summary>
    public class AgentFsm : StateMachine
    {
        public DataHolder DataHolder;

        public AgentFsm(DataHolder dataHolder)
        {
            this.DataHolder = dataHolder;
        }
    }
}