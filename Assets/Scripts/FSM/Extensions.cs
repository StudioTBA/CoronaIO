namespace Com.StudioTBD.CoronaIO.FMS.Extensions
{
    public static class Extension
    {
        public static void ChangeState(this State currentState, State newState)
        {
            currentState.StateMachine.ChangeState(newState);
        }
    }
}