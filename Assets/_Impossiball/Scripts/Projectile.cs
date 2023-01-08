using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    Rigidbody _rb;
    [SerializeField]
    float _bulletForce;
    public void FireProjectile(Transform target)
    {
        var directionToTarget = target.position - transform.position;
        _rb.AddForce(directionToTarget * _bulletForce);
    }
}
