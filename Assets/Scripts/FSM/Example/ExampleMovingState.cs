using System;
using UnityEngine;

public class ExampleWorkingState : IState
{
    public void OnStateEnter()
    {
        Debug.Log("Entering " + this.GetType().Name);
    }

    public void Execute()
    {
        Debug.Log("Executing " + this.GetType().Name);
    }

    public void OnStateExit()
    {
        Debug.Log("Exiting " + this.GetType().Name);
    }
}
