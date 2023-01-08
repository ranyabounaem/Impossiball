using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAggroState : State
{
    TurretAI _ai;
    RadarScan _radar;
    Transform _target;
    
    public TurretAggroState(TurretAI ai) 
    {
        _ai = ai;
        _radar = _ai.Radar;
        _target = _radar.CurrentDetectable;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _radar.OnDetection += HandleDetection;
        _ai.StartFiring(_target);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        _ai.TrackTarget(_target);
    }

    public override void OnExit()
    {
        base.OnExit();
        _radar.OnDetection -= HandleDetection;
    }

    void HandleDetection (IDetectable detectable)
    {
        if (detectable==null)
        {
            _ai.SetState(new TurretScanState(_ai));
        }
    }
}


