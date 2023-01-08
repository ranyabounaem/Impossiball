using System;
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
    [SerializeField]
    RadarScan _radar;
    [SerializeField]
    GameObject _bullet;

    [Header("References")]
    [SerializeField]
    Transform _bulletSpawnPos;

    public Transform UpperBase => _upperBase;
    public Transform Turret => _turret;
    public RadarScan Radar => _radar;

    void Awake()
    {
        SetState(new TurretScanState(this));
    }

    public void TrackTarget(Transform target)
    {
        RotateBase(target);
        RotateTurret(target);
    }    

    public void StartFiring(Transform target)
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        Instantiate(_bullet, _bulletSpawnPos.position, _turret.rotation);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Fire());
    }

    void RotateBase(Transform target)
    {
        var _playerPos = target.position;
        var _upperbasePos = _upperBase.position;
        Vector3 _directionToPlayer = (new Vector3(_playerPos.x, 0, _playerPos.z) - new Vector3(_upperbasePos.x, 0, _upperbasePos.z)).normalized;
        var _targetRotation = Quaternion.LookRotation(_directionToPlayer);
        var _angle = Quaternion.Angle(_targetRotation, _upperBase.rotation);
        _upperBase.rotation = Quaternion.RotateTowards(_upperBase.rotation, _targetRotation, 10 * _angle * Time.deltaTime);
    }

    void RotateTurret(Transform target)
    {
        var _playerPos = target.position;
        var _turretPos = _turret.position;
        Vector3 _directionToPlayer = (_playerPos - _turretPos).normalized;
        var _targetRotation = Quaternion.LookRotation(_directionToPlayer);
        var _angle = Quaternion.Angle(_targetRotation, _turret.rotation);
        _turret.rotation = Quaternion.RotateTowards(_turret.rotation, _targetRotation, 10 * _angle * Time.deltaTime);
    }


}
