using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TurretAI : AI
{
    [Header("Turret Parts")]
    [SerializeField]
    Transform _upperBase;
    [SerializeField]
    Transform _turret;

    [Header("Player Reference")]
    [SerializeField]
    Transform _player;

    void Awake()
    {
        SetState(new TurretScanState(this));
    }

    private void Update()
    {
        RotateBase();
        RotateTurret();
    }

    void RotateBase()
    {
        var _playerPos = _player.position;
        var _upperbasePos = _upperBase.position;
        Vector3 _directionToPlayer = (new Vector3(_playerPos.x, 0, _playerPos.z) - new Vector3(_upperbasePos.x, 0, _upperbasePos.z)).normalized;
        var _targetRotation = Quaternion.LookRotation(_directionToPlayer);
        var _angle = Quaternion.Angle(_targetRotation, _upperBase.rotation);
        _upperBase.rotation = Quaternion.RotateTowards(_upperBase.rotation, _targetRotation, 10 * _angle * Time.deltaTime);
    }

    void RotateTurret()
    {
        var _playerPos = _player.position;
        var _turretPos = _turret.position;
        Vector3 _directionToPlayer = (_playerPos - _turretPos).normalized;
        var _targetRotation = Quaternion.LookRotation(_directionToPlayer);
        var _angle = Quaternion.Angle(_targetRotation, _turret.rotation);
        _turret.rotation = Quaternion.RotateTowards(_turret.rotation, _targetRotation, 10 * _angle * Time.deltaTime);
    }
}
