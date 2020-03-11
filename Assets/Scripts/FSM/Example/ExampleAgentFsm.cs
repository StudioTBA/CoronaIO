using System;

namespace Com.StudioTBD.CoronaIO
{
    public class ExampleAgentFsm : StateMachine
    {
        protected override void Start()
        {
            base.Start();
            ChangeState(this.defaultState);
        }
    }
}