using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.FMS.Extensions
{
    public static class Extension
    {
        public static void ChangeState(this State currentState, State newState)
        {
            currentState.StateMachine.ChangeState(newState);
        }

        public static void Transition(this State currentState, State toState,
            Action<MonoBehaviour> action)
        {
            action.Invoke(currentState);
            ChangeState(currentState, toState);
        }
    }
}