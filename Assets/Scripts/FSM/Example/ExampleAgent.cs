using UnityEngine;
using System.Collections;

public class ExampleAgent : MonoBehaviour
{
    public Camera _camera;
    private StateMachine _stateMachine = new StateMachine();

    // Use this for initialization
    void Start()
    {
        _stateMachine.ChangeState(new ExampleIdleState());
    }
    
    // Update is called once per frame
    void Update()
    {
        _stateMachine.Update();

        
    }
}
