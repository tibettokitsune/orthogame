using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class MoveAndRotation : MonoBehaviour
{
    public bool IsGround { set => isGround = value; get => isGround; }

    [Inject] private IPlayerInput _playerInput;
    
    [Range(1, 10)] public float MaxVelocity;
    [Range(1, 1000)] public float Acceleration;
    [Range(1, 1000)] public float ForceGravity;
    [Range(1, 1000)] public float CoefMoveInAntigravity;
    
    [SerializeField] private GroundCheker GroundCheker;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private bool isGround;
    [SerializeField] private ObjectOfGravity currentObjectOfGravity;

    [SerializeField] private Rigidbody rigidbody;
    private Transform _cameraTransform;
    private void OnValidate()
    {
        if (!currentObjectOfGravity) currentObjectOfGravity = GetComponent<ObjectOfGravity>();
        if (!GroundCheker) GroundCheker = GetComponentInChildren<GroundCheker>();
        if(!rigidbody) rigidbody = GetComponent<Rigidbody>();
    }

    private void Initialize()
    {
        GroundCheker.OnGround += SetIsGround;
        _cameraTransform = Camera.main.transform;
    }

    private void OnDestroy()
    {
        GroundCheker.OnGround -= SetIsGround;
    }

    private void Start() => Initialize();

    private void Update()
    {
        if(_playerInput.JumpPressed()) Jump();

        CalculateMovementDirection();
    }

    private void CalculateMovementDirection()
    {
        var forward = _cameraTransform.forward;
        forward.y = 0;
        forward = forward.normalized;

        var right = _cameraTransform.right;
        right.y = 0;
        right = right.normalized;

        var top = Vector3.up;
        _direction = (forward * _playerInput.Vertical()
                      + right * _playerInput.Horizontal()
                      + top * _playerInput.Height());
    }

    private void FixedUpdate()
    {
        Rotation();
        Move();
        Gravity();
        SpeedLimit();
    }

    private void Move()
    {
        var tDirection = new Vector3(_direction.x, 0, _direction.z);

        if (currentObjectOfGravity.InSphere)
        {
            Vector3 direction = tDirection.normalized * Acceleration - rigidbody.velocity * 10;
            direction = direction / CoefMoveInAntigravity;

            rigidbody.AddForce(direction);
        }
        else
            rigidbody.AddForce(tDirection.normalized * Acceleration - rigidbody.velocity * 10, ForceMode.Force);

        var top = _direction.y;

        if (currentObjectOfGravity.VerticalPosition < 1) top = 1;
        currentObjectOfGravity.VerticalPosition += Time.deltaTime * top;
    }

    private void Gravity() {
            if(currentObjectOfGravity.InSphere == false)
                rigidbody.AddForce(Vector3.down * ForceGravity, ForceMode.Force);
    }

    private void SpeedLimit()
    {
        if (rigidbody.velocity.magnitude >= MaxVelocity)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * MaxVelocity;
        }
    }

    private void SetIsGround(bool value) {
        isGround = value;
    }
    private void Jump()
    {
        if (currentObjectOfGravity.InSphere) return;
        if (isGround)
        {
            rigidbody.AddForce(_direction.normalized + Vector3.up * 200, ForceMode.Impulse);
        }
    }

    private void Rotation()
    {
        if (_direction == Vector3.zero) return;
        Debug.DrawRay(transform.position, _direction *10);
        transform.forward = Vector3.Slerp(transform.forward, _direction, 10 * Time.deltaTime);
    }
}
