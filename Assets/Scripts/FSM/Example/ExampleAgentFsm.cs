using System;

namespace Com.StudioTBD.CoronaIO
{
    /// <summary>
    /// Example State Machine
    /// </summary>
    public class ExampleAgentFsm : StateMachine
    {
        protected override void Start()
        {
            base.Start();
            ChangeState(this.defaultState);
        }
    }
}