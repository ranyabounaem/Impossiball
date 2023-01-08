

public class TurretScanState : State
{
    TurretAI _ai;
    RadarScan _radar;
    public TurretScanState(TurretAI ai)
    {
        _ai = ai;
        _radar = _ai.Radar;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _radar.OnDetection += HandleDetection;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        _radar.OnDetection -= HandleDetection;
    }

    void HandleDetection(IDetectable detectable)
    {
        if (detectable != null)
        {
            _ai.SetState(new TurretAggroState(_ai));
        }
    }
}