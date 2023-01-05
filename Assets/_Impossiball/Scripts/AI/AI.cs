using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AI : MonoBehaviour
{
    public Transform bodyTransform;
    public Transform gunTransform;

    State currentState;
    public void SetState(State s)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = s;
        currentState.OnEnter();

    }

    void Update()
    {
        currentState.OnUpdate();
    }
}
