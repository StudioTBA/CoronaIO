using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.FMS.Example
{
    /// <summary>
    /// Example State Machine
    /// </summary>
    public class AgentFsm : StateMachine
    {
        [HideInInspector] public DataHolder DataHolder = new DataHolder();

        protected override void Start()
        {
            base.Start();
            ChangeState(this.defaultState);
        }
    }
}