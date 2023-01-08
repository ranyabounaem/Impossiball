using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AI : MonoBehaviour
{
    protected State _currentState;

    public void SetState(State s)
    {
        if (_currentState != null)
            _currentState.OnExit();
        _currentState = s;
        _currentState.OnEnter();

    }

    void Update()
    {
        _currentState.OnUpdate();
    }
}
