using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    IState currentState;

    public StateMachine()
    { }

    public StateMachine(IState initialState)
    {
        currentState = initialState;
    }


    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = newState;
        currentState.OnStateEnter();
    }

    public void Update()
    {
        if (currentState != null) currentState.Execute();
    }
}
