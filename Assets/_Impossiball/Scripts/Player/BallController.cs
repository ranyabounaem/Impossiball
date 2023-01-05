using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BallCollisionHandler(Collision collision);

public class BallController : MonoBehaviour, IDetectable
{
    [SerializeField] float _jumpPower = 100;
    [SerializeField] float _movementSpeed;
    [SerializeField] Transform _cameraTransform;

    bool _forwardMovementInput = false;
    bool _leftMovementInput = false;
    bool _rightMovementInput = false;
    bool _reverseMovementInput = false;
    bool _jumpInput = false;

    int _jumpsAllowed = 1;
    int _jumpsRemaining = 1;

    Rigidbody _rigidbody;

    public event BallCollisionHandler _OnBallCollided;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckForInput();
    }

    void FixedUpdate()
    {
        
        if (_forwardMovementInput)
        {
            var direction = _movementSpeed * _cameraTransform.forward;
            direction.y = 0;
            _rigidbody.AddForce(direction);
        }
            
        if (_leftMovementInput)
        {
            var direction = _movementSpeed * -_cameraTransform.right;
            direction.y = 0;
            _rigidbody.AddForce(direction);
        }
        if (_rightMovementInput)
        {
            var direction = _movementSpeed * _cameraTransform.right;
            direction.y = 0;
            _rigidbody.AddForce(direction);
        }
        if (_reverseMovementInput)
        {
            var direction = _movementSpeed * -_cameraTransform.forward;
            direction.y = 0;
            _rigidbody.AddForce(direction);
        }
        if (_jumpInput)
        {
            var direction = _jumpPower * _cameraTransform.up;
            _rigidbody.AddForce(direction);
        }
        ResetInputs();
    }

    void CheckForInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _forwardMovementInput = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _leftMovementInput = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rightMovementInput = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _reverseMovementInput = true;
        }
        if (Input.GetKey(KeyCode.Space) && _jumpsRemaining > 0)
        {
            _jumpInput = true;
            _jumpsRemaining--;
        }
    }

    void ResetInputs()
    {
        _forwardMovementInput = false;
        _leftMovementInput = false;
        _rightMovementInput = false;
        _reverseMovementInput = false;
        _jumpInput = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _jumpsRemaining = _jumpsAllowed;
        _OnBallCollided?.Invoke(collision);
    }
}
