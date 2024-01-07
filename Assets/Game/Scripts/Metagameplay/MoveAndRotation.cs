using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Metagameplay.Player;
using Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class MoveAndRotation : MonoBehaviour
{
    public bool IsGround { set => _isGround = value; get => _isGround; }
    public bool isMomentumStopped = false;
    [Inject] private PlayerConfiguration _configuration;
    [Inject] private GroundChecker _groundChecker;
    [Inject] private ObjectOfGravity _currentObjectOfGravity;
    [Inject] private Rigidbody _rigidbody;
    private Vector3 _direction;
    private bool _isGround;
    private Transform _cameraTransform;

    public float Horizontal, Vertical;
    private void OnDestroy()
    {
        _groundChecker.OnGround -= SetIsGround;
    }

    private void Start()
    {
        _groundChecker.OnGround += SetIsGround;
        _cameraTransform = Camera.main.transform;
    }

    public Vector3 CalculateDirection(Vector3 direction)
    {
        var forward = _cameraTransform.forward;
        forward.y = 0;
        forward = forward.normalized;

        var right = _cameraTransform.right;
        right.y = 0;
        right = right.normalized;

        var top = Vector3.up;

        _direction = (forward * direction.z
                      + right * direction.x
                      + top * direction.y);
        return _direction;
    }
    public void CalculateMovementDirection(Vector3 direction)
    {
        _direction = CalculateDirection(direction);
    }
    
    public void Jump()
    {
        if (_currentObjectOfGravity.InSphere) return;
        if (_isGround)
        {
            _rigidbody.AddForce(_direction.normalized + Vector3.up * 200, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Rotation();
        Move();
        Gravity();
        SpeedLimit();
        StopInetria();
    }

    void StopInetria()//float HorizontalInput, float VerticalInput)
    {/*
        bool isMaxInputHorizontal = Mathf.Abs(HorizontalInput) > 0.5;
        bool isMaxInputVertical = Mathf.Abs(VerticalInput) > 0.5;*/

        if (isMomentumStopped)
        {
            if (_groundChecker.IsGrounded && !_currentObjectOfGravity.InSphere)
                _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
        }
    }

    private void Move()
    {
        var tDirection = new Vector3(_direction.x, 0, _direction.z);

        if (_currentObjectOfGravity.InSphere)
        {
            Vector3 direction = tDirection.normalized * _configuration.acceleration - _rigidbody.velocity * 10;
            direction = direction / _configuration.antigravityMovementCoefficient;

            _rigidbody.AddForce(direction);
        }
        else
            _rigidbody.AddForce(tDirection.normalized * _configuration.acceleration - _rigidbody.velocity * 10, ForceMode.Force);

        var top = _direction.y;

        if (_currentObjectOfGravity.VerticalPosition < 1) top = 1;
        _currentObjectOfGravity.VerticalPosition += Time.deltaTime * top;
    }

    private void Gravity() {
            if(_currentObjectOfGravity.InSphere == false)
                _rigidbody.AddForce(Vector3.down * _configuration.forceGravity, ForceMode.Force);
    }

    private void SpeedLimit()
    {
        if (_rigidbody.velocity.magnitude >= _configuration.maxVelocity)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _configuration.maxVelocity;
        }
    }

    private void SetIsGround(bool value) {
        _isGround = value;
    }

    private void Rotation()
    {
        if (_direction == Vector3.zero) return;
        var xzDirection = new Vector3(_direction.x, 0, _direction.z);
        transform.forward = Vector3.Slerp(transform.forward, xzDirection, 10 * Time.deltaTime);
    }
}
