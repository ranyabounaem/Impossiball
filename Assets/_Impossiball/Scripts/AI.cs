using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class State
{
    protected AI _ai;
    public State(AI ai)
    {
        _ai = ai;
    }
    public virtual void OnEnter()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}

public class TurretIdleState : State
{
    public TurretIdleState(AI ai):base(ai)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        ResetTurretRotation();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    void ResetTurretRotation()
    {
        _ai.gunTransform.rotation = Quaternion.identity;
        _ai.bodyTransform.rotation = Quaternion.identity;
    }
}
public class AI : MonoBehaviour
{
    public Transform bodyTransform;
    public Transform gunTransform;

    State currentState;

    void Awake()
    {
        SetState(new TurretIdleState(this));
    }

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
